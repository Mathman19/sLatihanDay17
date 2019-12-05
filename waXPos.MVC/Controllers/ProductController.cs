using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPos.DataAccess;
using XPos.ViewModel;

namespace waXPos.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<ProductViewModel> listProduct = ProductRepo.All();
            return PartialView("_List", listProduct);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            ViewBag.VariantList = new SelectList(VariantRepo.ByCategory(0), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            HttpPostedFileBase file = model.File;
            string[] fileName = file.FileName.Split('.');
            string newFile = DateTime.Now.ToString("yyyyMMddHHmmss") + "."
                        + fileName[fileName.Length - 1];
            model.Image = newFile;
            ResponResult result = ProductRepo.Update(model);
            if (result.Success)
            {
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/FileUpload"),
                                                   Path.GetFileName(newFile));
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
            }
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long Id)
        {
            ProductViewModel model = ProductRepo.ById(Id);
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            ViewBag.VariantList = new SelectList(VariantRepo.ByCategory(model.CategoryId), "Id", "Name");
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            ResponResult result = ProductRepo.Update(model);
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
            ProductViewModel model = ProductRepo.ById(id);
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            ViewBag.VariantList = new SelectList(VariantRepo.ByCategory(model.CategoryId), "Id", "Name");
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProductViewModel model)
        {
            ResponResult result = ProductRepo.Delete(model.Id);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductList(string search = "")
        {
            List<ProductViewModel> result = ProductRepo.ByFilter(search);
            return PartialView("_ProductList", result);
        }

        //public ActionResult UploadFile()
        //{
        //    return PartialView("_UploadFile");
        //}

        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    ResponResult result = new ResponResult();
        //    if (file != null && file.ContentLength > 0)
        //        try
        //        {
        //            string[] fileName = file.FileName.Split('.');
        //            string newFile = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileName[fileName.Length - 1];
        //            string path = Path.Combine(Server.MapPath("~/UploadFile"),
        //                                       Path.GetFileName(newFile));
        //            file.SaveAs(path);
        //            ViewBag.Message = "File uploaded successfully";
        //            ViewBag.UploadedFile = "\\UploadFile\\" + newFile;
        //            result.Entity = fileName[0];
        //            result.Success = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "ERROR:" + ex.Message.ToString();
        //            result.Success = false;
        //        }
        //    else
        //    {
        //        ViewBag.Message = "You have not specified a file.";
        //    }
        //    return Json(new
        //    {
        //        name = result.Entity,
        //        success = result.Success
        //    },JsonRequestBehavior.AllowGet);
        //}
    }
}