
using Nolan.Web.Models;

namespace Nolan.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResonseDto> SendAsync(RequestDto requestDto);
    }
}
