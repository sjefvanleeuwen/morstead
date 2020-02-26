using System;
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

        public void UpSert(Parameter parameter)
        {
            var foundParameter = FindKnowParameter(parameter);
            //new parameter: add
            if (foundParameter == null)
            {
                Add(parameter);
            }
            //known parameter: update Value
            else
            {
                foundParameter.Value = parameter.Value;
            }
        }

        private Parameter FindKnowParameter(Parameter searchParameter)
        {
            return this.FirstOrDefault(p => p.Name == searchParameter.Name /*&& p.Key == searchParameter.Key*/);
        }

    }
}
