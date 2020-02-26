using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ParametersCollection : List<IParameter>
    {
        public Parameter GetParameter(string name)
        {
            return (Parameter)(from p in this where p.Name == name select p).SingleOrDefault();
        }
    }
}
