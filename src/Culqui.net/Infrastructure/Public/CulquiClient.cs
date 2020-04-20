using Culqui.Entities;
using Culqui.Entities.Base;
using Culqui.Entities.Interfaces;
using Culqui.Exceptions;
using Culqui.Infrastructure.Interfaces;
using Culqui.Services.Base;
using Culqui.Services.Common;
using Culqui.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui.Infrastructure.Public
{
    public class CulquiClient : ICulquiClient
    {
        public CulquiClient(string publicApiKey = null, string secretApiKey = null, ICulquiHttpClient httpClient = null, string apiBase = null)
        {
            if (publicApiKey != null && publicApiKey.Length == 0)
            {
                throw new ArgumentException("API key cannot be the empty string.", nameof(publicApiKey));
            }

            if (publicApiKey != null && StringUtils.ContainsWhitespace(publicApiKey))
            {
                throw new ArgumentException("API key cannot contain whitespace.", nameof(publicApiKey));
            }

            if (secretApiKey != null && secretApiKey.Length == 0)
            {
                throw new ArgumentException("API key cannot be the empty string.", nameof(secretApiKey));
            }

            if (secretApiKey != null && StringUtils.ContainsWhitespace(secretApiKey))
            {
                throw new ArgumentException("API key cannot contain whitespace.", nameof(secretApiKey));
            }

            PublicApiKey = publicApiKey;
            SecretApiKey = secretApiKey;
            HttpClient = httpClient ?? BuildDefaultHttpClient();
            ApiBase = apiBase ?? DefaultApiBase;
        }

        public static string DefaultApiBase => "https://secure.culqi.com/v2";
        public string ApiBase { get; }
        public string PublicApiKey { get; }
        public string SecretApiKey { get; }
        public ICulquiHttpClient HttpClient { get; }

        public async Task<T> RequestAsync<T>(
            HttpMethod method, 
            string path, 
            BaseOptions options,
            RequestOptions requestOptions,
            CancellationToken cancellationToken = default) where T : ICulquiEntity
        {
            var request = new CulquiRequest(this, method, path, options, requestOptions);
            var response = await this.HttpClient.MakeRequestAsync(request, cancellationToken)
                .ConfigureAwait(false);
            return ProcessResponse<T>(response);
        }

        private static ICulquiHttpClient BuildDefaultHttpClient()
        {
            return new CulquiHttpClient();
        }

        private static T ProcessResponse<T>(CulquiResponse response) where T : ICulquiEntity
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw BuildCulquiException(response);
            }

            T obj;
            try
            {
                obj = CulquiEntity.FromJson<T>(response.Content);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw BuildInvalidResponseException(response);
            }

            obj.CulquiResponse = response;
            return obj;
        }

        private static CulquiException BuildCulquiException(CulquiResponse response)
        {
            JObject jObject = null;

            try
            {
                jObject = JObject.Parse(response.Content);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return BuildInvalidResponseException(response);
            }

            var errorToken = jObject["error"];
            if (errorToken == null)
            {
                return BuildInvalidResponseException(response);
            }

            var culquiError = errorToken.Type == JTokenType.String
                ? CulquiError.FromJson(response.Content)
                : CulquiError.FromJson(errorToken.ToString());

            culquiError.CulquiResponse = response;

            return new CulquiException(
                response.StatusCode,
                culquiError,
                culquiError.Message ?? culquiError.UserMessage)
            {
                CulquiResponse = response,
            };
        }

        private static CulquiException BuildInvalidResponseException(CulquiResponse response)
        {
            return new CulquiException(response.StatusCode, null, $"Invalid response object from API: \"{response.Content}\"")
            {
                CulquiResponse = response,
            };
        }
    }
}
