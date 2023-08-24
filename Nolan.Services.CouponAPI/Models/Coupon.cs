using System.ComponentModel.DataAnnotations;

namespace Nolan.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string? CouponCode { get; set; }
        public double DiscountAmount { get; set; } = 0;
        public int MinAmount { get; set;} = 0;
    }
}
