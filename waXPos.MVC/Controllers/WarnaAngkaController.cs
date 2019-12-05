using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waXPos.MVC.Controllers
{
    public class WarnaAngkaController : Controller
    {
        // GET: WarnaAngka
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Green(long val)
        {
            ViewBag.Value = val;
            return PartialView("_Green");
        }


        public ActionResult Yellow(long val)
        {
            ViewBag.Value = val;
            return PartialView("_Yellow");
        }

        public ActionResult Blue(long val)
        {
            ViewBag.Value = val;
            return PartialView("_Blue");
        }

        public ActionResult Orange(long val)
        {
            ViewBag.Value = val;
            return PartialView("_Orange");
        }
    }
}