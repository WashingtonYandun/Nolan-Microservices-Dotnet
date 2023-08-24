using System.ComponentModel.DataAnnotations;

namespace Nolan.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public double DiscountAmount { get; set; } = 0;
        public int MinAmount { get; set; } = 0;
    }
}
