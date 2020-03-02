using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class RechtHelper
    {
        public static bool HasRecht(ExecutionResult result)
        {
            return (!result.Parameters.Any(p => p.Name == "recht" && !(bool)p.Value));
        }
    }
}
