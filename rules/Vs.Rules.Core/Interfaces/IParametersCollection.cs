using System.Collections.Generic;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Core.Interfaces
{
    public interface IParametersCollection : IList<IParameter>
    {
        IParameter GetParameter(string name);

        IEnumerable<IParameter> GetAll();
        void UpSert(IParameter parameter);
    }
}