using Culqui.Entities.Interfaces;
using Culqui.Services.Base;
using Culqui.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui.Services.Interfaces
{
    public interface IRetrievable <TEntity, TOptions> where TEntity : ICulquiEntity, IHasId where TOptions : BaseOptions, new()
    {
        Task<TEntity> Get(string id, TOptions retrieveOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
