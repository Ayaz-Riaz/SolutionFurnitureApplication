using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
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
        CategoriesServices categoryServices = new CategoriesServices();
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
            CategoriesServices categoryService = new CategoriesServices();

            var categories = categoryServices.GetCategories();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            CategoriesServices categoryService = new CategoriesServices(); 

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = categoryService.GetCategory(model.CategoryID);

            productsServices.SaveProduct(newProduct);

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