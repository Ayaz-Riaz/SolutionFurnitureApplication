using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FurnitureApplication.Services
{
    public class ProductsServices
    {
        public Product GetProduct(int ID)
        {
            using (var context = new FAContext())
            {
                return context.Products.Where(x=>x.ID == ID).Include(x=>x.Category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new FAContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();

            }

        }
        public List<Product> GetProducts()
        {

            using (var context = new FAContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
            }
        }

        //********************Save Products

        public void SaveProduct(Product Product)
        {
            using (var context = new FAContext())
            {
                context.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged;

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
