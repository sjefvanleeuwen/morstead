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

        public void UpSert(IParameter parameter, IEnumerable<string> correspondingParameterNames = null)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }
            correspondingParameterNames ??= new List<string>
            {
                parameter.Name
            };
            var correspondingParameter = FindCorrespondingParameter(correspondingParameterNames);
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

        /// <summary>
        /// There can be multiple names that represent a value, for instance in case of boolean
        /// i.e alleenstaande = ja should replace samenwonend_met_toeslagpartner = ja
        /// </summary>
        private IParameter FindCorrespondingParameter(IEnumerable<string> correspondingParameterNames)
        {
            foreach (var p in this)
            {
                if (correspondingParameterNames.ToList().Contains(p.Name))
                {
                    return p;
                }
            }

            return null;
        }
    }
}
