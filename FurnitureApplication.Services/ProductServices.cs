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
        #region singleton
        public static ProductsServices Instance
        {
            get
            {
                if (instance == null) instance = new ProductsServices();
                return instance;
            }
        }
        private static ProductsServices instance { get; set; }
        private ProductsServices()
        {
        }
        #endregion


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
        public List<Product> GetProducts(int pageNo)
        {
            //int pageSize = 5;
            using (var context = new FAContext())
            {
                //return context.Products.OrderBy(x=>x.ID).Skip((pageNo-1) * pageSize).Take(pageSize).Include(x=>x.Category).ToList();


                return context.Products.Include(x => x.Category).ToList();
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
