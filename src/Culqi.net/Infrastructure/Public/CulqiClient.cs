using Culqi.Entities;
using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Exceptions;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using Culqi.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Infrastructure.Public
{
    public class CulqiClient : ICulqiClient
    {
        public CulqiClient(string publicApiKey = null, string secretApiKey = null, ICulqiHttpClient httpClient = null, string apiBase = null)
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
        public ICulqiHttpClient HttpClient { get; }

        public async Task<T> RequestAsync<T>(
            HttpMethod method, 
            string path, 
            BaseOptions options,
            RequestOptions requestOptions,
            CancellationToken cancellationToken = default) where T : ICulqiEntity
        {
            var request = new CulqiRequest(this, method, path, options, requestOptions);
            var response = await this.HttpClient.MakeRequestAsync(request, cancellationToken)
                .ConfigureAwait(false);
            return ProcessResponse<T>(response);
        }

        private static ICulqiHttpClient BuildDefaultHttpClient()
        {
            return new CulqiHttpClient();
        }

        private static T ProcessResponse<T>(CulqiResponse response) where T : ICulqiEntity
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw BuildCulqiException(response);
            }

            T obj;
            try
            {
                obj = CulqiEntity.FromJson<T>(response.Content);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw BuildInvalidResponseException(response);
            }

            obj.CulqiResponse = response;
            return obj;
        }

        private static CulqiException BuildCulqiException(CulqiResponse response)
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

            var culqiError = errorToken.Type == JTokenType.String
                ? CulqiError.FromJson(response.Content)
                : CulqiError.FromJson(errorToken.ToString());

            culqiError.CulqiResponse = response;

            return new CulqiException(
                response.StatusCode,
                culqiError,
                culqiError.Message ?? culqiError.UserMessage)
            {
                CulqiResponse = response,
            };
        }

        private static CulqiException BuildInvalidResponseException(CulqiResponse response)
        {
            return new CulqiException(response.StatusCode, null, $"Invalid response object from API: \"{response.Content}\"")
            {
                CulqiResponse = response,
            };
        }
    }
}
