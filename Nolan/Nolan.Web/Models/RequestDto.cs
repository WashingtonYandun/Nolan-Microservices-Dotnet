using Nolan.Web.Utils;
using System.Net;

namespace Nolan.Web.Models
{
    public class RequestDto
    {
        public ApiMethod Method { get; set; } = ApiMethod.GET;
        public string Url { get; set; }
        public object? Data { get; set; }
        public string AccessToken { get; set; }
    }
}
