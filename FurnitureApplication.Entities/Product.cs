using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }

        //public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
        
    }
}
