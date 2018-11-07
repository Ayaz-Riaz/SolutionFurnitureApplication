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
        public ActionResult CategoryTable(string search, int? pageNo, bool asShared = false)
        {
            ViewBag.UseAsShared = asShared;
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesServices.Instance.GetCategoriesCount(search);
            model.Categories = CategoriesServices.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);

                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
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
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesServices.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageUrl = category.ImageUrl;
            model.isFeatured = category.IsFeatured;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesServices.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageUrl = model.ImageUrl;
            existingCategory.IsFeatured = model.isFeatured;

            CategoriesServices.Instance.UpdateCategory(existingCategory);

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