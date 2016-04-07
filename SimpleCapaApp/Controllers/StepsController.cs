using SimpleCapaApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCapaApp.CusClasses;

namespace SimpleCapaApp.Controllers
{
    public class StepsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Steps
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContainmentTasks(int CapaId)
        {
            ViewBag.CapaId = CapaId;
            ViewBag.CapaStep = 2;
            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 2).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);
            return View(tasks.ToList());
        }

        public ActionResult FinishContainmentTasks(int CapaId)
        {
            Capa capa = db.Capas.Find(CapaId);
            capa.Step = capa.Step + 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Capas", new { id = CapaId});
        }

        public ActionResult Containment(int CapaId)
        {

            ViewBag.CapaId = CapaId;

            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 2).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);

            var total = tasks.ToList().Count();
            var completed = tasks.Where(t => t.Status == Status.Completed).Count();

            if (total == completed)
            {
                ViewBag.CompletedStatus = true;
            }
            else
            {
                ViewBag.CompletedStatus = false;
            }

            return View(tasks.ToList());
        }

        public ActionResult FinishContainment(int CapaId)
        {
            Capa capa = db.Capas.Find(CapaId);
            capa.Step = capa.Step + 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Capas", new { id = CapaId });
        }

        public ActionResult CorrectionTasks(int CapaId)
        {
            ViewBag.CapaId = CapaId;
            ViewBag.CapaStep = 4;
            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 4).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);
            return View(tasks.ToList());
        }

        

        public ActionResult FinishCorrectionTasks(int CapaId)
        {
            Capa capa = db.Capas.Find(CapaId);
            capa.Step = capa.Step + 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Capas", new { id = CapaId });
        }

        public ActionResult Correction(int CapaId)
        {

            ViewBag.CapaId = CapaId;

            var tasks = db.Tasks.Where(t => t.CapaId == CapaId && t.CapaStep == 4).Include(t => t.Capa).Include(t => t.Technitian).Include(t => t.Files);

            var total = tasks.ToList().Count();
            var completed = tasks.Where(t => t.Status == Status.Completed).Count();

            if (total == completed)
            {
                ViewBag.CompletedStatus = true;
            }
            else
            {
                ViewBag.CompletedStatus = false;
            }

            return View(tasks.ToList());

        }

        public ActionResult FinishCorrection(int CapaId)
        {
            Capa capa = db.Capas.Find(CapaId);
            capa.Step = capa.Step + 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Capas", new { id = CapaId });
        }

        

        public ActionResult Verification(int id)
        {

            return View();
        }

        public ActionResult Finish(int id)
        {

            return View();
        }


    }

}