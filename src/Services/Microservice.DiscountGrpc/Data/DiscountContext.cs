

using Microservice.DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DiscountGrpc.Data
{
    public class DiscountContext:DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().ToTable("Coupons");
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Product1", Description = "Discount for Product1", Amount = 10 },
                new Coupon { Id = 2, ProductName = "Product2", Description = "Discount for Product2", Amount = 20 },
                new Coupon { Id = 3, ProductName = "Product3", Description = "Discount for Product3", Amount = 30 }
            );
        }
    }
}