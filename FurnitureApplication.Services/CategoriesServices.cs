using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Services
{
    public class CategoriesServices
    {
        #region singleton
        public static CategoriesServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesServices();
                return instance;
            }
        }
        private static CategoriesServices instance { get; set; }
        private CategoriesServices()
        {
        }
        #endregion

        public Category GetCategory(int ID)
        {
            using (var context = new FAContext())
            {
                return context.Categories.Find(ID);
            }
        }

        //********************Get category Count

        public int GetCategoriesCount(string search)
        {
            using (var context = new FAContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null && category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        //********************Get All category

        public List<Category> GetAllCategories()
        {
            using (var context = new FAContext())
            {
                return context.Categories.ToList();
            }
        }

        //********************Get category

        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;

            using (var context = new FAContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                         category.Name.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.ID)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .Include(x => x.Products)
                         .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }

        //********************Feature category

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new FAContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageUrl != null).ToList();
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
                var categoty = context.Categories.Where(x=>x.ID == ID).Include(x=>x.Products).FirstOrDefault();

                context.Products.RemoveRange(categoty.Products);

                context.Categories.Remove(categoty);

                context.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new FAContext())
            {
                return context.Categories.Include(r=>r.Products).ToList();
            }
        }
        
    }
}
