using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCapaApp.Models;
using SimpleCapaApp.CustomFilters;
using System.Web.Security;

namespace SimpleCapaApp.Controllers
{
    public class CapasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Capas
        public ActionResult Index()
        {
            var capas = db.Capas.Include(c => c.Supervisor);  
            return View(capas.ToList());
        }

       // [AuthLog(Roles = "Supervisor")]
        public ActionResult SupervisorCapas(string id)
        {
            return View(db.Capas.Where(c => c.UserId == id).ToList());
        }


        // GET: Capas/Details/5
       // [AuthLog(Roles = "Supervisor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capa capa = db.Capas.Find(id);
            if (capa == null)
            {
                return HttpNotFound();
            }
            return View(capa);
        }

        // GET: Capas/Create
        //[AuthLog(Roles = "Administrador")]
        //[AuthLog(Roles = "Supervisor")]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("d78386b1-92f3-4d53-b171-ea9e8ba68e0e")) ,"Id", "Email");
            return View();
        }

        // POST: Capas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthLog(Roles = "Administrador")]
        //[AuthLog(Roles = "Supervisor")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,UserId,CreationDate,DueDate")] Capa capa)
        {
            if (ModelState.IsValid)
            {
                capa.Step = 2;
                db.Capas.Add(capa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", capa.UserId);
            return View(capa);
        }

        // GET: Capas/Edit/5
       // [AuthLog(Roles = "Administrador")]
        //[AuthLog(Roles = "Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capa capa = db.Capas.Find(id);
            if (capa == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", capa.UserId);
            return View(capa);
        }

        // POST: Capas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthLog(Roles = "Administrador")]
        //[AuthLog(Roles = "Supervisor")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,UserId,CreationDate,DueDate")] Capa capa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", capa.UserId);
            return View(capa);
        }

        // GET: Capas/Delete/5
       // [AuthLog(Roles = "Administrador")]
        //[AuthLog(Roles = "Supervisor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capa capa = db.Capas.Find(id);
            if (capa == null)
            {
                return HttpNotFound();
            }
            return View(capa);
        }

        // POST: Capas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capa capa = db.Capas.Find(id);
            db.Capas.Remove(capa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VerifyCapa(int CapaId)
        {
            var capa = db.Capas.Find(CapaId);
            return View(capa);
        }


        public ActionResult FinishCapaVerification(int CapaId)
        {
            var capa = db.Capas.Find(CapaId);
            capa.Status = capa.Status + 1;
            capa.Step = capa.Step + 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Capas", new { id = CapaId });
        }

        public ActionResult FollowUpCapa(int CapaId)
        {
            var capa = db.Capas.Find(CapaId);
            return View(capa);
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
