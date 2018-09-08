using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly IStorage _localStorage;
        public RestClient(HttpClient httpClient, IStorage localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> GetValues()
        {
            var result = await _httpClient.GetAsync("http://localhost:17176/api/values");
            return await result.Content.ReadAsStringAsync();
        }

        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var random = new Random().Next(100);
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(_sleepDurations, (delegateResult, timespan, context) =>
                {
                    _localStorage.Set("access_token", random.ToString());
                });
        }
        private TimeSpan[] _sleepDurations = new[] { TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) };
    }
}