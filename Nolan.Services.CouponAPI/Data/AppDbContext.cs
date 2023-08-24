using Microsoft.EntityFrameworkCore;
using Nolan.Services.CouponAPI.Models;

namespace Nolan.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10OAA",
                DiscountAmount = 20,
                MinAmount = 100,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20OBB",
                DiscountAmount = 20,
                MinAmount = 100,
            });
        }
    }
}
