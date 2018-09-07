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
        private readonly ITokenStorage _tokenStorage;
        public RestClient(HttpClient httpClient, ITokenStorage tokenStorage)
        {
            _httpClient = httpClient;
            _tokenStorage = tokenStorage;
        }

        public async Task<string> GetValues()
        {
            var result = await _httpClient.GetAsync("http://localhost:17176/api/values");
            return await result.Content.ReadAsStringAsync();
        }

        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(_sleepDurations, (delegateResult, timespan, context) =>
                {
                    _tokenStorage.Set("zizhong-zhang");
                });
        }
        private TimeSpan[] _sleepDurations = new[] { TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) };
    }
}