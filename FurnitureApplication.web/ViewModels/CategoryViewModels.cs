using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurnitureApplication.web.ViewModels
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class NewCategoryViewModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }

        public bool HasDiscount { get; set; }

        public double DiscountPercentage { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryID { get; set; }

        public bool isFeatured { get; set; }
    }

    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool isFeatured { get; set; }
    }
}