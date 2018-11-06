using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FurnitureApplication.web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult index()
        {
            var categries = CategoriesServices.Instance.GetCategories();

            return View(categries);
        }
        public ActionResult CategoryTable(string search, int? pageNoh)
        {
            var categories = CategoriesServices.Instance.GetCategories();
            if (string.IsNullOrEmpty(search) == false)
            {
                categories = categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            var categorySearchVM = new CategorySearchViewModel
            {
                Categories = categories,
                SearchTerm = search,
                Pager = new Pager(categories.Count, pageNoh, 10)

            };
            return PartialView(categorySearchVM);
        }

        //*****************Create option

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageUrl = model.ImageUrl;
            newCategory.IsFeatured = model.isFeatured;
            CategoriesServices.Instance.SaveCategory(newCategory);
            return RedirectToAction("index");
        }

        //*****************Edit option

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var category = CategoriesServices.Instance.GetCategory(ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoriesServices.Instance.UpdateCategory(category);
            return RedirectToAction("index");
        }


        //*****************Delete option

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category = CategoriesServices.Instance.GetCategory(ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            category = CategoriesServices.Instance.GetCategory(category.ID);

            CategoriesServices.Instance.DeleteCategory(category.ID);
            return RedirectToAction("index");
        }
    }
}