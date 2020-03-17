using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ParametersCollection : List<IParameter>, IParametersCollection
    {
        public IParameter GetParameter(string name)
        {
            return (from p in this where p.Name == name select p).SingleOrDefault();
        }

        public IEnumerable<IParameter> GetAll()
        {
            return this;
        }

        public void UpSert(IParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            this.RemoveAll(p => p.Name == parameter.Name);
            this.Add(parameter);
        }
    }
}
