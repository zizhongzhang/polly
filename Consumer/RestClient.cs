using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;
        public RestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetValues()
        {
            var result = await _httpClient.GetAsync("http://localhost:17176/api/values");
            return await result.Content.ReadAsStringAsync();
        }
    }
}