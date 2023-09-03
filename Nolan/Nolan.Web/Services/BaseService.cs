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

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("NolanAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDto.Url);

                switch (requestDto.Method)
                {
                    case ApiMethod.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case ApiMethod.POST:
                        message.Method = HttpMethod.Post;
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                        break;
                    case ApiMethod.PUT:
                        message.Method = HttpMethod.Put;
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                        break;
                    case ApiMethod.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                var response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto>(content);

                return result;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = new HttpResponseMessage(HttpStatusCode.InternalServerError).ToString(),
                };
                var response = JsonConvert.SerializeObject(dto);
                var content = new StringContent(response, Encoding.UTF8, "application/json");
                var result = JsonConvert.DeserializeObject<ResponseDto>(response);
                return result;
            }
        }
    }
}
