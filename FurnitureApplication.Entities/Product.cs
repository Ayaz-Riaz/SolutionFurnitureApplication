using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public bool HasDiscount { get; set; }
        public double DiscountPercentage { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryID { get; set; }

        public string ImageUrl { get; set; }
    }
}
