using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class Shop1Controller : Controller
    {
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsServices.Instance.GetMaximumPrice();

            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID);

            return View(model);
        }
        // GET: Shop1
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsServices.Instance.GetProducts(model.CartProductIDs);
            }

            return View(model);
        }
    }
}