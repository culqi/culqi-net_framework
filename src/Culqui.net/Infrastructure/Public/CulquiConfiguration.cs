using Culqui.Exceptions;
using Culqui.Infrastructure;
using Culqui.Infrastructure.Interfaces;
using Culqui.Infrastructure.Public;
using Culqui.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace Culqui
{
    public class CulquiConfiguration
    {
        private static string publicApiKey;

        private static string secretApiKey;

        private static ICulquiClient culquiClient;

        static CulquiConfiguration()
        {
            CulquiNetVersion = new AssemblyName(typeof(CulquiConfiguration).GetTypeInfo().Assembly.FullName).Version.ToString(3);
        }

        public static string PublicApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(publicApiKey) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CulquiPublicApiKey"]))
                {
                    publicApiKey = ConfigurationManager.AppSettings["CulquiPublicApiKey"];
                }

                return publicApiKey;
            }

            set
            {
                if (value != publicApiKey)
                {
                    culquiClient = null;
                }

                publicApiKey = value;
            }
        }

        public static string SecretApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(secretApiKey) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CulquiSecretApiKey"]))
                {
                    secretApiKey = ConfigurationManager.AppSettings["CulquiSecretApiKey"];
                }

                return secretApiKey;
            }

            set
            {
                if (value != secretApiKey)
                {
                    culquiClient = null;
                }

                secretApiKey = value;
            }
        }

        public static JsonSerializerSettings SerializerSettings { get; set; } = DefaultSerializerSettings();

        public static ICulquiClient CulquiClient
        {
            get
            {
                if (culquiClient == null)
                {
                    culquiClient = BuildDefaultCulquiClient();
                }

                return culquiClient;
            }

            set => culquiClient = value;
        }

        public static string CulquiNetVersion { get; }

        public static JsonSerializerSettings DefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new CulquiObjectConverter(),
                },
                DateParseHandling = DateParseHandling.None,
            };
        }

        private static CulquiClient BuildDefaultCulquiClient()
        {
            if (PublicApiKey != null && PublicApiKey.Length == 0)
            {
                var message = "Your Public API key is invalid, as it is an empty string. You can "
                    + "double-check your API key from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            if (PublicApiKey != null && StringUtils.ContainsWhitespace(PublicApiKey))
            {
                var message = "Your Public API key is invalid, as it contains whitespace. You can "
                    + "double-check your API key from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            if (SecretApiKey != null && SecretApiKey.Length == 0)
            {
                var message = "Your Secret API key is invalid, as it is an empty string. You can "
                    + "double-check your API key from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            if (SecretApiKey != null && StringUtils.ContainsWhitespace(SecretApiKey))
            {
                var message = "Your Secret API key is invalid, as it contains whitespace. You can "
                    + "double-check your API key from the Culqui Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulquiException(message);
            }

            return new CulquiClient(PublicApiKey, SecretApiKey, new CulquiHttpClient(httpClient: null));
        }
    }
}
