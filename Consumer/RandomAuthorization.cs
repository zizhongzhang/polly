using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class RandomAuthorization : DelegatingHandler
    {
        public RandomAuthorization()
        {
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var random = new Random().Next(100);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("basic", random.ToString());
            return base.SendAsync(request, cancellationToken);
        }
    }
}