using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts)
        {
            ProductsWidgetsViewModel model = new ProductsWidgetsViewModel();
            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                model.Products = ProductsServices.Instance.GetLatestProducts(4);
            }
            else
            {
                model.Products = ProductsServices.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }
    }
}