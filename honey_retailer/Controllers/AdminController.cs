using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using honey_retailer.Models;

namespace honey_retailer.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProjectRepository repository = new ProjectRepository();
        public ActionResult Index()
        {
            return View(repository.Categories);
        }

        public ActionResult EditCategory(int categoryId = 0)
        {
            Category category = repository.Categories.FirstOrDefault(c => c.Id == categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product productForDelete = repository.DeleteProduct(productId);
            if (productForDelete != null)
            {
                TempData["message"] = string.Format("Товар {0} удален", productForDelete.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCategory(Category category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                if (upload != null)
                {
                    string imageName = "category_img_" + category.Id + ".jpg";
                    upload.SaveAs(Server.MapPath("~/Images/" + imageName));
                    category.ImageName = imageName;
                    repository.SaveCategory(category);
                }
                TempData["message"] = string.Format("Категория {0} сохранена", category.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult EditProduct(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                if (upload != null)
                {
                    string imageName = "product_img_" + product.Id + ".jpg";
                    upload.SaveAs(Server.MapPath("~/Images/" + imageName));
                    product.ImageName = imageName;
                    repository.SaveProduct(product);
                }
                TempData["message"] = string.Format("Товар {0} сохранен", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult CreateCategory()
        {
            ViewBag.New = true;
            return View("EditCategory", new Category());
        }

        public ActionResult CreateProduct(int categoryId)
        {
            ViewBag.New = true;
            return View("EditProduct", new Product());
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category categoryForDelete = repository.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            repository.DeleteCategory(categoryForDelete.Id);
            if (categoryForDelete != null)
            {
                TempData["message"] = string.Format("Категория {0} удалена", categoryForDelete.Name);
            }
            return RedirectToAction("Index");
        }
    }
}