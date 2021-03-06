﻿using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search, int? pageNo = 1)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo > 0 ? pageNo.Value : 1 : 1 ;

            var totalRecords = ProductsServices.Instance.GetProductsCount(search);
            model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, pageSize);
            if(model.Products != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, pageSize);
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
            
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
            newProduct.DiscountPercentage = model.DiscountPercentage;
            newProduct.HasDiscount = model.HasDiscount;
            newProduct.OriginalPrice = model.OriginalPrice;

            if(newProduct.DiscountPercentage > 0)
            {
                newProduct.HasDiscount = true;
                var discountAmount = (decimal)(newProduct.DiscountPercentage / 100.0) * newProduct.OriginalPrice;
                newProduct.Price = newProduct.OriginalPrice - discountAmount;
            }


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

            model.DiscountPercentage = product.DiscountPercentage;
            model.HasDiscount = product.HasDiscount;
            model.OriginalPrice = product.OriginalPrice;

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

            existingProduct.DiscountPercentage = model.DiscountPercentage;
            existingProduct.HasDiscount = model.HasDiscount;
            existingProduct.OriginalPrice = model.OriginalPrice;

            if (model.HasDiscount && model.DiscountPercentage >= 0)
            {
                existingProduct.HasDiscount = true;
                var discountAmount = (decimal)(model.DiscountPercentage / 100.0) * model.OriginalPrice;
                existingProduct.Price = model.OriginalPrice - discountAmount;
            }else
            {
                existingProduct.HasDiscount = false;
                existingProduct.OriginalPrice = model.Price;
                existingProduct.DiscountPercentage = 0; 

            }

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                existingProduct.ImageUrl = model.ImageUrl;
            }

            ProductsServices.Instance.UpdateProduct(existingProduct);

            return new HttpStatusCodeResult(200);
        }


        //**********Detail
        //[HttpGet]
        //public ActionResult Details(int ID)
        //{
        //    ProductViewModel model = new ProductViewModel();

        //    model.Product = ProductsServices.Instance.GetProduct(ID);

        //    return View(model);
        //}


        //****************delete Product

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsServices.Instance.DeleteProduct(ID);
            
            return RedirectToAction("index");
        }

    }
}