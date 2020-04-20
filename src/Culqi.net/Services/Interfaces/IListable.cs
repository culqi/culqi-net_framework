using Culqi.Entities;
using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Services.Interfaces
{
    public interface IListable <TEntity, TOptions> where TEntity : ICulqiEntity, IHasId where TOptions : ListOptions, new()
    {
        Task<CulqiList<TEntity>> List(TOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);        

#if !NET45
        IAsyncEnumerable<TEntity> ListAutoPaging(TOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
#else
        IEnumerable<TEntity> ListAutoPaging(TOptions listOptions = null, RequestOptions requestOptions = null);
#endif
    }
}
