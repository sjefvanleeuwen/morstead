using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class RechtHelper
    {
        public static bool HasRecht(ExecutionResult result)
        {
            var rechtParam = result.Parameters.FirstOrDefault(p => p.Name == "recht");
            return rechtParam == null || (bool)rechtParam.Value == true;
        }
    }
}
