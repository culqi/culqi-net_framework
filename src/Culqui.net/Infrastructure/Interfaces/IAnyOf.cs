using System;
using System.Collections.Generic;
using System.Text;

namespace Culqui.Infrastructure.Interfaces
{
    public interface IAnyOf
    {
        object Value { get; }
        Type Type { get; }
    }
}
