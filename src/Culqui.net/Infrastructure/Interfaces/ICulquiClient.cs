using Culqui.Entities.Interfaces;
using Culqui.Services.Base;
using Culqui.Services.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui.Infrastructure.Interfaces
{
    public interface ICulquiClient
    {
        string ApiBase { get; }
        string PublicApiKey { get; }
        string SecretApiKey { get; }
        Task<T> RequestAsync<T>(
            HttpMethod method, 
            string path,
            BaseOptions options,
            RequestOptions requestOptions, 
            CancellationToken cancellationToken = default) where T : ICulquiEntity;
    }
}
