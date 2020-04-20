using Culqui.Entities;
using Culqui.Infrastructure.Interfaces;
using Culqui.Services.Base;
using Culqui.Services.Common;
using Culqui.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui
{
    public class TokenService : Service<Token>, 
        ICreatable<Token, TokenCreateOptions>, 
        IRetrievable<Token, TokenGetOptions>, 
        IListable<Token, TokenListOptions>,
        IUpdatable<Token, TokenUpdateOptions>
    {
        public TokenService() : base(null)
        {
        }

        public TokenService(ICulquiClient client) : base(client)
        {
        }

        public override string BasePath => "/tokens";

        public virtual Task<Token> Create(TokenCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Token> Get(string tokenId, TokenGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(tokenId, options, requestOptions, cancellationToken);
        }

        public Task<CulquiList<Token>> List(TokenListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

#if !NET45
        public IAsyncEnumerable<Token> ListAutoPaging(TokenListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntitiesAutoPaging(listOptions, requestOptions, cancellationToken);
        }
#else
        public IEnumerable<Token> ListAutoPaging(TokenListOptions listOptions = null, RequestOptions requestOptions = null)
        {
            return ListEntitiesAutoPaging(listOptions, requestOptions);
        }
#endif

        public Task<Token> Update(string id, TokenUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(id, updateOptions, requestOptions, cancellationToken);
        }
    }
}
