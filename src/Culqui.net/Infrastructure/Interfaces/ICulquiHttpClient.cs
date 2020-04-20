using Culqui.Infrastructure.Public;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqui.Infrastructure.Interfaces
{
    public interface ICulquiHttpClient
    {
        Task<CulquiResponse> MakeRequestAsync(CulquiRequest request, CancellationToken cancellationToken = default);
    }
}
