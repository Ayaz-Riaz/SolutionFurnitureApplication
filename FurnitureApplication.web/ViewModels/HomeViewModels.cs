using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureApplication.web.ViewModels
{
    public class HomeViewModels
    {
        //public List<Category> FeatureedCategories { get; set; }
        public List<Product> Featuredproducts { get; set; }

        public List<Product> HasDiscount { get; set; }

        public List<Category> FeaturedCategories { get; set; }
    }
}