using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Entities
{
    public class Order
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public DateTime ordereddAt { get; set; }

        public string status { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
