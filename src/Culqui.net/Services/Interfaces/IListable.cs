using Culqui.Entities;
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
    public interface IListable <TEntity, TOptions> where TEntity : ICulquiEntity, IHasId where TOptions : ListOptions, new()
    {
        Task<CulquiList<TEntity>> List(TOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);        

#if !NET45
        IAsyncEnumerable<TEntity> ListAutoPaging(TOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
#else
        IEnumerable<TEntity> ListAutoPaging(TOptions listOptions = null, RequestOptions requestOptions = null);
#endif
    }
}
