using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>().HasKey(x => new { x.CategoryId, x.ProductId });

            #region Seed Data 

            modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Email = "bakhtiarimoghadam1383@gmail.com",
                Password = "Admin1234@",
                IsAdmin = true
            });

            modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = 1,
                Title = "موبایل",
            });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Quantity = 5,
                    Price = 1100,
                    Title = "Samsung S25",
                    Description = "Samsung"
                },
                new Product()
                {
                    Id = 2,
                    Quantity = 3,
                    Price = 1000,
                    Title = "Iphone 16",
                    Description = "Apple"
                });

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct()
                {
                    CategoryId = 1,
                    ProductId = 1
                },
                new CategoryToProduct()
                {
                    CategoryId = 1,
                    ProductId = 2
                });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
