using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Common
{
    public class RequestOptions
    {
        public string PublicApiKey { get; set; }
        public string SecretApiKey { get; set; }
        internal string BaseUrl { get; set; }
        internal string CulqiVersion { get; set; }
    }
}
