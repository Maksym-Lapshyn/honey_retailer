using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using honey_retailer.Models;

namespace honey_retailer.Controllers
{
    public class HomeController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Index()
        {
            IEnumerable<Category> topCategories = repository.Categories;
            return View(topCategories);
        }

        public ActionResult Categories()
        {
            return View(repository.Categories);
        }

        public ActionResult Category(int categoryId)
        {
            Category cat = repository.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            ViewBag.CategoryName = cat.Name;
            ViewBag.ImageName = cat.ImageName;
            ViewBag.Name = cat.Name;
            return View(repository.Products.Where(p => p.CategoryId == categoryId).ToList());
        }

        public ActionResult Product(int productId)
        {
            Product prod = repository.Products.Where(p => p.Id == productId).FirstOrDefault();
            return View(prod);
        }
    }
}