using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesServices categoryService = new CategoriesServices();
        // GET: Category
        [HttpGet]
        public ActionResult index()
        {
            var categries = categoryService.GetCategories();

            return View(categries);
        }

        //*****************Create option

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.SaveCategory(category);

            return RedirectToAction("index");
        }

        //*****************Edit option

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var category = categoryService.GetCategory(ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("index");
        }


        //*****************Delete option

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category = categoryService.GetCategory(ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            category = categoryService.GetCategory(category.ID);

            categoryService.DeleteCategory(category.ID);
            return RedirectToAction("index");
        }
    }
}