using Culqui.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui.Infrastructure.Public
{
    public class CulquiHttpClient : ICulquiHttpClient
    {

        private static readonly Lazy<HttpClient> LazyDefaultHttpClient = new Lazy<HttpClient>(BuildDefaultSystemNetHttpClient);
        private readonly HttpClient httpClient;

        public CulquiHttpClient(HttpClient httpClient = null)
        {
#if NET45
            // With .NET Framework 4.5, it's necessary to manually enable support for TLS 1.2.
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol |
                SecurityProtocolType.Tls12;
#endif

            this.httpClient = httpClient ?? LazyDefaultHttpClient.Value;
        }

        public static TimeSpan DefaultHttpTimeout => TimeSpan.FromSeconds(80);

        public static HttpClient BuildDefaultSystemNetHttpClient()
        {
            return new HttpClient
            {
                Timeout = DefaultHttpTimeout,
            };
        }

        public async Task<CulquiResponse> MakeRequestAsync(CulquiRequest request, CancellationToken cancellationToken = default)
        {
            Exception requestException = null;
            HttpResponseMessage response = null;
            requestException = null;

            var httpRequest = BuildRequestMessage(request);

            try
            {
                response = await httpClient.SendAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                requestException = exception;
            }
            catch (OperationCanceledException exception)
                when (!cancellationToken.IsCancellationRequested)
            {
                requestException = exception;
            }

            if (requestException != null)
            {
                throw requestException;
            }

            string contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new CulquiResponse(response.StatusCode, response.Headers, contentResponse);
        }

        private HttpRequestMessage BuildRequestMessage(CulquiRequest request)
        {
            var requestMessage = new HttpRequestMessage(request.Method, request.Uri);
            requestMessage.Headers.Authorization = request.AuthorizationHeader;
            requestMessage.Content = request.Content;
            return requestMessage;
        }
    }
}
