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
    public interface IUpdatable<TEntity, TOptions> where TEntity : ICulquiEntity, IHasId where TOptions : BaseOptions, new()
    {
        Task<TEntity> Update(string id, TOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
