using Polly;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    public interface IRestClient
    {
        Task<string> GetValues();
        Task<string> GetEndpoint(string endpoint);
        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
    }
}