using Newtonsoft.Json;
using Nolan.Web.Models;
using Nolan.Web.Services.IServices;
using Nolan.Web.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Nolan.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResonseDto> SendAsync(RequestDto requestDto)
        {
            var client = _httpClientFactory.CreateClient("NolanApi");
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Add("Accept", "application/json");
            request.RequestUri = new Uri(requestDto.Url);

            switch (requestDto.ApiType)
            {
                case ApiType.GET:
                    request.Method = HttpMethod.Get;
                    break;
                case ApiType.POST:
                    request.Method = HttpMethod.Post;
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                    break;
                case ApiType.PUT:
                    request.Method = HttpMethod.Put;
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                    break;
                case ApiType.DELETE:
                    request.Method = HttpMethod.Delete;
                    break;
                default:
                    request.Method = HttpMethod.Get;
                    break;
            }

            if (!string.IsNullOrEmpty(requestDto.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", requestDto.AccessToken);
            }

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var resonseDto = new ResonseDto();
            resonseDto.Result = content;
            if (!response.IsSuccessStatusCode)
            {
                resonseDto.IsSuccess = false;
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    resonseDto.Message = "Please login again";
                }
                else if (statusCode == HttpStatusCode.Forbidden)
                {
                    resonseDto.Message = "You are not authorized";
                }
            }
            return resonseDto;
        }
    }
}
