namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class FormulaSituation : ISituation
    {
        private readonly string _name;

        public FormulaSituation(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public void Accept(ISituationVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}