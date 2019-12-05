using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPos.DataAccess;
using XPos.ViewModel;

namespace waXPos.MVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectedProduct(long Id)
        {
            ProductViewModel product = ProductRepo.ById(Id);
            OrderDetailViewModel detail = new OrderDetailViewModel();
            detail.ProductId = product.Id;
            detail.ProductName = product.Name;
            detail.Price = product.Price;
            return PartialView("_SelectedProduct", detail);
        }

        public ActionResult Payment(OrderHeaderViewModel model)
        {
            return PartialView("_Payment", model);
        }

        public ActionResult Pay(OrderHeaderViewModel model)
        {
            ResultOrder result = OrderRepo.Post(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                reference = result.Reference,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}