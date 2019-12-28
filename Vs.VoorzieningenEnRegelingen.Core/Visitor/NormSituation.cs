using System;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class NormSituation : ISituation
    {
        private readonly string _name;

        public NormSituation(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name => _name;

        public void Accept(ISituationVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}