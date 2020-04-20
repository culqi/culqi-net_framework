using Culqui.Exceptions;
using Culqui.Infrastructure.FormEncoding;
using Culqui.Infrastructure.Interfaces;
using Culqui.Services.Base;
using Culqui.Services.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Culqui.Infrastructure.Public
{
    public class CulquiRequest
    {
        private readonly BaseOptions options;
        public CulquiRequest(ICulquiClient client, HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.options = options;
            Method = method;
            Uri = BuildUri(client, method, path, options, requestOptions);
            AuthorizationHeader = BuildAuthorizationHeader(client, method, path);
        }

        public HttpMethod Method { get; }
        public Uri Uri { get; }
        public AuthenticationHeaderValue AuthorizationHeader { get; }
        public HttpContent Content => BuildContent(Method, options);

        public override string ToString()
        {
            return string.Format("<{0} Method={1} Uri={2}>", GetType().FullName, Method, Uri.ToString());
        }

        private static Uri BuildUri(ICulquiClient client, HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions)
        {
            var b = new StringBuilder();

            b.Append(requestOptions?.BaseUrl ?? client.ApiBase);
            b.Append(path);

            if ((method != HttpMethod.Post) && (options != null))
            {
                var queryString = FormEncoder.CreateQueryString(options);
                if (!string.IsNullOrEmpty(queryString))
                {
                    b.Append("?");
                    b.Append(queryString);
                }
            }

            return new Uri(b.ToString());
        }

        private static AuthenticationHeaderValue BuildAuthorizationHeader(ICulquiClient client, HttpMethod method, string path)
        {
            string publicApiKey = client.PublicApiKey;
            string secretApiKey = client.SecretApiKey;
            string apiToUse = client.SecretApiKey;

            if (publicApiKey == null)
            {
                var message = "No API key provided. Set your public API key using "
                    + "`CulquiConfiguration.PublicApiKey = \"<API-KEY>\"`. You can generate API keys "
                    + "from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            if (secretApiKey == null)
            {
                var message = "No API key provided. Set your secret API key using "
                    + "`CulquiConfiguration.SecretApiKey = \"<API-KEY>\"`. You can generate API keys "
                    + "from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            if ((method == HttpMethod.Post) && (path == "tokens"))
            {
                apiToUse = publicApiKey;
            }

            return new AuthenticationHeaderValue("Bearer", apiToUse);
        }

        private static HttpContent BuildContent(HttpMethod method, BaseOptions options)
        {
            if (method != HttpMethod.Post)
            {
                return null;
            }

            return FormEncoder.CreateHttpContent(options);
        }
    }
}
