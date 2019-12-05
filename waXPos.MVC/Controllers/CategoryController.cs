using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPos.DataAccess;
using XPos.ViewModel;

namespace waXPos.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<CategoryViewModel> listCategories = CategoryRepo.All();
            return PartialView("_List", listCategories);
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            ResponResult result = CategoryRepo.Update(model);
            return Json(
                new { success = result.Success,
                      message = result.Message,
                      entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            CategoryViewModel model = CategoryRepo.ById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            ResponResult result = CategoryRepo.Update(model);
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
            CategoryViewModel model = CategoryRepo.ById(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CategoryViewModel model)
        {
            ResponResult result = CategoryRepo.Delete(model.Id);
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