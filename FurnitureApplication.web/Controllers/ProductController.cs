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
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.PageNo = pageNo.HasValue ? pageNo > 0 ? pageNo.Value : 1 : 1 ;

            //like above code both same reacion
            //if (pageNo.HasValue)
            //{
            //    if(pageNo.Value > 0)
            //    {
            //        model.PageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        model.PageNo = 1;
            //    }
            //}
            //else
            //{
            //    model.PageNo = 1;
            //}

            model.Products = ProductsServices.Instance.GetProducts(model.PageNo);

            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(model);
        }

        //****************create Product

        [HttpGet]
        public ActionResult Create()
        {

            var categories = CategoriesServices.Instance.GetCategories();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = CategoriesServices.Instance.GetCategory(model.CategoryID);
            newProduct.ImageUrl = model.ImageUrl;

            ProductsServices.Instance.SaveProduct(newProduct);

            return RedirectToAction("index");
        }

        //****************Edit Product

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsServices.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageUrl = product.ImageUrl;

            model.AvailableCategories = CategoriesServices.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsServices.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                existingProduct.ImageUrl = model.ImageUrl;
            }

            ProductsServices.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("index");
        }


        //**********Detail
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsServices.Instance.GetProduct(ID);

            return View(model);
        }


        //****************delete Product

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsServices.Instance.DeleteProduct(ID);

            return RedirectToAction("index");
        }

    }
}