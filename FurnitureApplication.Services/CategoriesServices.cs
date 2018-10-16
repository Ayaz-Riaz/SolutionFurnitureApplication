using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Services
{
    public class CategoriesServices
    {
        public Category GetCategory(int ID)
        {
            using (var context = new FAContext())
            {
                return context.Categories.Find(ID);
            }
        }
        public List<Category> GetCategories()
        {
            using (var context = new FAContext())
            {
                return context.Categories.ToList();
            }
        }

        //********************Save category

        public void SaveCategory(Category category)
        {
            using (var context = new FAContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        //********************Update category


        public void UpdateCategory(Category category)
        {
            using (var context = new FAContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //********************Delete category*****
         
        public void DeleteCategory(int ID)
        {
            using (var context = new FAContext())
            {
                var categoty = context.Categories.Find(ID);

                context.Categories.Remove(categoty);

                context.SaveChanges();
            }
        }
    }
}
