using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ParametersCollection : List<Parameter>
    {
        public Parameter GetParameter(string name)
        {
            return (from p in this where p.Name == name select p).SingleOrDefault();
        }
    }
}
