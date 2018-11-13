using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsServices.Instance.GetProduct(ID);

            return View(model);
        }
    }
}