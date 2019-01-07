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
        private static string Base => "https://localhost:17176/api/";
        private static string Retry => "retry/";
        public RestClient(HttpClient httpClient, IStorage localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> GetValues()
        {
            var result = await _httpClient.GetAsync(Base + "values");
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetEndpoint(string endpoint)
        {
            var result = await _httpClient.GetAsync(Base + Retry + endpoint);
            return await result.Content.ReadAsStringAsync();
        }

        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var random = new Random().Next(100);
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(SleepDurations, (delegateResult, timespan, context) =>
                {
                    _localStorage.Set("access_token", random.ToString());
                });
        }
        public static TimeSpan[] SleepDurations => new[] { TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) };
    }
}