﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCapaApp.Models;

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

        public ActionResult IdentificationTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Identification).ToList());
        }

        public ActionResult InvestigationTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Investigation).ToList());
        }

        public ActionResult PlanTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Plan).ToList());
        }

        public ActionResult CorrectionTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Correction).ToList());
        }

        public ActionResult ImplementationTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Implementation).ToList());
        }

        public ActionResult PreventionTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Prevention).ToList());
        }

        public ActionResult VerifyTasks(int id)
        {

            return View(db.Tasks.Where(c => c.CapaId == id && c.Step == Step.Verify).ToList());
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
        public ActionResult Create()
        {
            ViewBag.CapaId = new SelectList(db.Capas, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("ff821b87-9dc5-4b74-ba42-30a5fa1df290")), "Id", "Email");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,UserId,CapaId,Status,Step,CreationDate,DueDate")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CapaId = new SelectList(db.Capas, "Id", "Name", task.CapaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", task.UserId);
            return View(task);
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