using FurnitureApplication.Services;
using FurnitureApplication.web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FurnitureApplication.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (Request.IsAuthenticated)
            //{
                
            //    RolePrincipal r = (RolePrincipal)User;
            //    var isAdmin = r.GetRoles();/*?.Contains("Admin");*/
            //    return RedirectToAction("Index", "Admin");
            //}

            HomeViewModels model = new HomeViewModels();

            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}