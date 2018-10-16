using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class ProductController : Controller
    {
        ProductsServices productsServices = new ProductsServices();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search)
        {

            var products = productsServices.GetProducts();
            if (string.IsNullOrEmpty(search) == false) { 
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(products);
        }

        //****************create Product

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            productsServices.SaveProduct(product);

            return RedirectToAction("ProductTable");
        }


        //****************Edit Product

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productsServices.GetProduct(ID);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsServices.UpdateProduct(product);

            return RedirectToAction("ProductTable");
        }

        //****************delete Product

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsServices.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }

    }
}