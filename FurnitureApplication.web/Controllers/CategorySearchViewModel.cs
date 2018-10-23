using System.Collections.Generic;
using System.Web.UI;
using FurnitureApplication.Entities;

namespace FurnitureApplication.web.Controllers
{
    internal class CategorySearchViewModel
    {
        public List<Category> Categories { get; internal set; }
        public string SearchTerm { get; internal set; }
        public Pair Pager { get; internal set; }
    }
}