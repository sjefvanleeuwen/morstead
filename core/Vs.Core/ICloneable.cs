using System;
using System.Collections.Generic;
using System.Text;

namespace Vs.Core
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}
