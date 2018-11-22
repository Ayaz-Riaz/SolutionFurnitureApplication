using FurnitureApplication.Entities;
using FurnitureApplication.Services;
using FurnitureApplication.web.Models;
using FurnitureApplication.web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class Shop1Controller : Controller
    {


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult CheckoutDetail()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsServices.Instance.GetProducts(model.CartProductIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());

            }

            return View(model);
        }
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();
            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsServices.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            return View(model);

        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();
            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsServices.Instance.GetProducts(model.CartProductIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());

            }

            return View(model);
        }
        public JsonResult PlaceOrder(CheckoutViewModel model, string productIDs)
        {
            model.User = UserManager.FindById(User.Identity.GetUserId());
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();
                var boughtProducts = ProductsServices.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.orderedAt = DateTime.Now;
                newOrder.status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantities.Where(productID => productID == x.ID).Count() }));

           
                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);
                result.Data = new { Success = true, Rows = rowsEffected };

                //*********Email sending code

                var fromEmail = new MailAddress("rastgroup1@gmail.com", "Rast Group");
                var toEmail = new MailAddress(model.User.Email);
                var fromEmailPassword = "ayaz123456"; //replace with actual Password

                string subject = "Order";
                string body = "Your Order Placed successfully";
                
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {

                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}