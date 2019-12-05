using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waXPos.MVC.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Addition(int a, int b)
        {
            int result = a+ b;
            return Json(
                new
                {
                    hasil = result
                },
                JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Subs(int a, int b)
        {
            int result = a - b;
            return Json(
                new
                {
                    hasil = result
                },
                JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Multi(int a, int b)
        {
            int result = a * b;
            return Json(
                new
                {
                    hasil = result
                },
                JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Devide(decimal a, decimal b)
        {
            decimal result = a / b;
            return Json(
                new
                {
                    hasil = result
                },
                JsonRequestBehavior.DenyGet);
        }
    }
}