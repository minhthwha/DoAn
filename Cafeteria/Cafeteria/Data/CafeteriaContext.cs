using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Models;

namespace Cafeteria.Data
{
    public class CafeteriaContext : DbContext
    {
        public CafeteriaContext(DbContextOptions<CafeteriaContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;
        public DbSet<Ingredient> Ingredients { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<DrinkItem> DrinkItems { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed categories.
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, CategoryName = "Cà phê" },
                new { CategoryId = 2, CategoryName = "Trà" },
                new { CategoryId = 3, CategoryName = "Sữa" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
