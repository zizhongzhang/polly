using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class TokenAuthorization : DelegatingHandler
    {
        private readonly IStorage _localStorage;
        public TokenAuthorization(IStorage localStorage)
        {
            _localStorage = localStorage;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _localStorage.Get<string>("access_token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("basic", token);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}