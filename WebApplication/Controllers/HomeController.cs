using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using photoResizer;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ConfigModel model = new ConfigModel
            {
                InputFolder = "C:\\Users\\uladzimir_artsemenka\\Downloads\\input\\",
                OutputFolder = "C:\\Users\\uladzimir_artsemenka\\Downloads\\output\\",
                ThreadCapacity = 50,
                ThreadCount = 4,
                Resolutions = "240x320;600x600;120x360;800x600"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Run(ConfigModel model)
        {
            var photoManager = PhotoFactory.GetManager(model);
            Session["PhotoManager"] = photoManager;
            photoManager?.Resize();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Stop()
        {
            var photoManager = Session["PhotoManager"] as PhotoManager;
            photoManager?.Abort();
            return Json("Stopped!");
        }
    }
}