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
    public interface ICreatable<TEntity, TOptions>where TEntity : ICulquiEntity where TOptions : BaseOptions, new()
    {
        Task<TEntity> Create(TOptions createOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
