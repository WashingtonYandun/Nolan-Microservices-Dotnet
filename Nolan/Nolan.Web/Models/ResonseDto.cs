namespace Nolan.Web.Models
{
    public class ResonseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public string Message { get; set; } = "Success";
    }
}
