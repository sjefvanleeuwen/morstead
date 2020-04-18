namespace Vs.Rules.Core.Model
{
    public class Situation : ISituation
    {
        public string Expression { get; }

        public Situation(string expression)
        {
            Expression = expression ?? throw new System.ArgumentNullException(nameof(expression));
        }
    }
}