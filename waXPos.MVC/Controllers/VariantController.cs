using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPos.DataAccess;
using XPos.ViewModel;

namespace waXPos.MVC.Controllers
{
    public class VariantController : Controller
    {
        // GET: Variant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<VariantViewModel> listVariant = VariantRepo.All();
            return PartialView("_List", listVariant);
        }

        public ActionResult ListByCategory(long Id = 0)
        {
            // id => category id
            return PartialView("_ListByCategory", VariantRepo.ByCategory(Id));
        }

        //public ActionResult ListByCategoryJson(long Id = 0)
        //{
        //    List<VariantViewModel> result = VariantRepo.ByCategory(Id);
        //    return Json(new
        //    {
        //        hasil = result
        //    },
        //    JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(VariantViewModel model)
        {
            ResponResult result = VariantRepo.Update(model);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long Id)
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            VariantViewModel model = VariantRepo.ById(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(VariantViewModel model)
        {
            ResponResult result = VariantRepo.Update(model);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            VariantViewModel model = VariantRepo.ById(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(VariantViewModel model)
        {
            ResponResult result = VariantRepo.Delete(model.Id);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}