using System;
using System.Collections.Generic;
using System.Linq;
using honey_retailer.Models;
using System.Web;
using System.Web.Mvc;

namespace honey_retailer.Models
{
    public class ProjectRepository
    {
        private ProjectContext pc = new ProjectContext();
        public IEnumerable<Product> Products { get { return pc.Products; } }
        public IEnumerable<Category> Categories { get { return pc.Categories; } }

        public Category DeleteCategory(int categoryId)
        {
            Category categoryForDelete = pc.Categories.Find(categoryId);
            if (categoryForDelete != null)
            {
                IEnumerable<Product> productsForDelete = pc.Products.Where(p => p.CategoryId == categoryForDelete.Id);
                if (categoryForDelete.Products.Count > 0)
                {
                    foreach (Product prod in categoryForDelete.Products)
                    {
                        DeleteImage(prod.Id, false);
                    }
                }
                pc.Categories.Remove(categoryForDelete);
                pc.Products.RemoveRange(productsForDelete);
                DeleteImage(categoryId, true);
                pc.SaveChanges();
            }
            return categoryForDelete;
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                pc.Categories.Add(category);
            }
            else
            {
                Category forSave = pc.Categories.Find(category.Id);
                if (forSave != null)
                {
                    forSave.Name = category.Name;
                    forSave.ImageName = category.ImageName;
                    forSave.Products = category.Products;
                }
            }
            pc.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product productForDelete = pc.Products.Find(productId);
            if (productForDelete != null)
            {
                DeleteImage(productId, false);
                Category categoryToRemoveProductFrom = pc.Categories.Where(c => c.Products.Contains(productForDelete)).FirstOrDefault();
                pc.Categories.Find(categoryToRemoveProductFrom).Products.Remove(productForDelete);
                pc.Products.Remove(productForDelete);
                pc.SaveChanges();
            }
            return productForDelete;
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                pc.Products.Add(product);
            }
            else
            {
                Product forSave = pc.Products.Find(product.Id);
                if (forSave != null)
                {
                    forSave.Name = product.Name;
                    forSave.Description = product.Description;
                    forSave.ImageName = product.ImageName;
                    forSave.CategoryId = product.CategoryId;
                    forSave.Price = product.Price;
                    forSave.Category = product.Category;
                }
            }
            pc.SaveChanges();
        }

        private void DeleteImage(int id, bool category)
        {
            string path;
            if (category)
            {
                path = HttpContext.Current.Server.MapPath("~/Images/category_img_" + id + ".jpg");
            }
            else
            {
                path = HttpContext.Current.Server.MapPath("~/Images/product_img_" + id + ".jpg");
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}