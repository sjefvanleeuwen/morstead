using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ParametersCollection : List<IParameter>
    {
        public IParameter GetParameter(string name)
        {
            return (from p in this where p.Name == name select p).SingleOrDefault();
        }

        public void UpSert(IParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }
            
            var correspondingParameter = FindCorrespondingParameter(parameter.Name);
            if (correspondingParameter != null)
            {
                //update the parameter
                //only name and value required, corresponding name-value pairs for parameters will always be of the same type.
                correspondingParameter.Name = parameter.Name;
                correspondingParameter.Value = parameter.Value;
                correspondingParameter.ValueAsString = parameter.ValueAsString;
                return;
            }
            Add(parameter);
        }

        private IParameter FindCorrespondingParameter(string parameterName)
        {
            return this.FirstOrDefault(p => p.Name == parameterName);
        }
    }
}
