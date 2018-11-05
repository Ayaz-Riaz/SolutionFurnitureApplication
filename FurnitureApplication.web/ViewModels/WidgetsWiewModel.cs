using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureApplication.web.ViewModels
{
    public class ProductsWidgetsViewModel
    {
        public List<Product> Products { get; set; }

        public bool IsLatestProducts { get; set; }
    }
}