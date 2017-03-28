namespace honey_retailer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class ProjectContext : DbContext
    {
        // Your context has been configured to use a 'ProjectContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'honey_retailer.Models.ProjectContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProjectContext' 
        // connection string in the application configuration file.
        public ProjectContext()
            : base("name=ProjectContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "¬ведите им€ категории")]
        public string Name { get; set; }

        public string ImageName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }

    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "¬ведите им€ товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "¬ведите описание")]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "¬ведите положительное число")]
        public double Price { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}