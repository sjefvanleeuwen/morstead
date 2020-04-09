using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core.Interfaces
{
    public interface IParametersCollection : IList<IParameter>
    {
        IParameter GetParameter(string name);

        IEnumerable<IParameter> GetAll();
        void UpSert(IParameter parameter);
    }
}