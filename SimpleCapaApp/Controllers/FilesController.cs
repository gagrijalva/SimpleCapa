using SimpleCapaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCapaApp.Controllers
{
    public class FilesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Files
        public ActionResult Index()
        {

            var files = db.Files.ToList();

            return View(files);
        }

        public ActionResult Save(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                string TaskId = Request.Form["TaskId"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    int taskId = Convert.ToInt32(TaskId);
                    file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    var entity = new Models.File
                    {
                        FileName = fileName,
                        ContentType = fileContentType,
                        fileBytes = fileBytes,
                        TaskId = taskId
                    };

                    db.Files.Add(entity);
                    db.SaveChanges();
                }
            }

            var files = db.Files.ToList();

            return RedirectToAction("Index", "Tasks", null);

        }

        public ActionResult GetFIle(int Id) {

            var file = db.Files.Find(Id);
            var result = new FileContentResult(file.fileBytes, file.ContentType);
            result.FileDownloadName = file.FileName;

            return result;
       
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