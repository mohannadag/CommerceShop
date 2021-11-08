using Commerce.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasKey(c => new { c.Guid, c.Culture });
            builder.Entity<Category>().HasKey(c => new { c.Guid, c.Culture });
            builder.Entity<Product>()
                     .HasOne(e => e.Category)
                     .WithMany(e => e.Products)
                     .HasForeignKey(e => new { e.CategoryId, e.Culture });
        }
    }
}
