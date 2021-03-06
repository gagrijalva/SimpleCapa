﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCapaApp.Models;
using WebApp.Utilities;
using System.Net.Mail;

namespace SimpleCapaApp.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);
            return View(tasks.ToList());
        }


        public ActionResult OverDueTasks()
        {
            return View(db.Tasks.Where(t => t.DueDate <= DateTime.Now && t.Status != Status.Completed).ToList());
        }

        public ActionResult Completed()
        {
            return View(db.Tasks.Where(t => t.Status == Status.Completed));
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create(int CapaId, int CapaStep)
        {
            ViewBag.CapaId = CapaId;
            ViewBag.CapaStep = CapaStep;
            ViewBag.UserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("b7100b03-d6b6-409d-b5e3-b0b2c4e0235a")), "Id", "Email");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,UserId,DueDate,CapaId,CapaStep")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.Status = Status.New;
                task.CreationDate = DateTime.Now;
                db.Tasks.Add(task);
                db.SaveChanges();
            }

            ViewBag.CapaId = new SelectList(db.Capas, "Id", "Name", task.CapaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", task.UserId);
            return RedirectToAction("Details", "Capas", new { id = task.CapaId });
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.CapaId = new SelectList(db.Capas, "Id", "Name", task.CapaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", task.UserId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,UserId,CapaId,Status,Step,CreationDate,DueDate")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CapaId = new SelectList(db.Capas, "Id", "Name", task.CapaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", task.UserId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ContainmentTasksSummary(int CapaId)
        {
            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 2).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);
            return View(tasks.ToList());
        }

        public ActionResult CorrectionTasksSummary(int CapaId)
        {
            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 4).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);
            return View(tasks.ToList());
        }

        public ActionResult FinishTask(int TaskId, string TechnitianId)
        {
            var task = db.Tasks.Find(TaskId);
            var supervisorEmail = task.Capa.Supervisor.Email;
            var technitianEmail = task.Technitian.Email;
            task.Status = Status.Completed;
            string emailMessage = technitianEmail + " has completed a task. Details: \n" +
                "TaskId: " + TaskId + "\n" +
                "CapaId: " + task.CapaId + "\n" +
                "Due Date: " + task.DueDate;

            db.SaveChanges();
            MailSender.Send("Completed Task", emailMessage  , new MailAddress(supervisorEmail));
            return RedirectToAction("Technitian", "UserPanel", new { UserId = TechnitianId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
