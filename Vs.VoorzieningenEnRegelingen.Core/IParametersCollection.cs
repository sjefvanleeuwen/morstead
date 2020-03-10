using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IParametersCollection : IList<IParameter>
    {
        IParameter GetParameter(string name);
        void UpSert(IParameter parameter);
    }
}