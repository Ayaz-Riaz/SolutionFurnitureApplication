using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
