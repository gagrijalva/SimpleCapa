using SimpleCapaApp.CustomFilters;
using SimpleCapaApp.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCapaApp.Controllers
{
    public class UserPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPanel
        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult Administrator()
        {
            return View();
        }


        public ActionResult Supervisor()
        {
            return View();
        }
        
        public ActionResult Technitian()
        {
            return View();
        }
    }
}