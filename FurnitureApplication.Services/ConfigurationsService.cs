using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Services
{
    public class ConfigurationsService
    {
        public Config GetConfig(string Key)
        {
            using (var context = new FAContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
