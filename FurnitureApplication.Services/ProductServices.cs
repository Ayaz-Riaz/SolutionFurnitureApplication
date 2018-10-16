using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Services
{
    public class ProductsServices
    {
        public Product GetProduct(int ID)
        {
            using (var context = new FAContext())
            {
                return context.Products.Find(ID);
            }
        }
        public List<Product> GetProducts()
        {
            using (var context = new FAContext())
            {
                return context.Products.ToList();
            }
        }

        //********************Save Products

        public void SaveProduct(Product Product)
        {
            using (var context = new FAContext())
            {
                context.Products.Add(Product);
                context.SaveChanges();
            }
        }

        //********************Update Products


        public void UpdateProduct(Product Product)
        {
            using (var context = new FAContext())
            {
                context.Entry(Product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //********************Delete Products

        public void DeleteProduct(int ID)
        {
            using (var context = new FAContext())
            {
                var Product = context.Products.Find(ID);

                context.Products.Remove(Product);

                context.SaveChanges();
            }
        }
    }
}
