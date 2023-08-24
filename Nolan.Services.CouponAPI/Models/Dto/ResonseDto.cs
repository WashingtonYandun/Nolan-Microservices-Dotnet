namespace Nolan.Services.CouponAPI.Models.Dto
{
    public class ResonseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public string Message { get; set; } = "";
    }
}
