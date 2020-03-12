using System;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core.Helpers
{
    public static class YamlHelper
    {
        public static string GetFormulaNameFromStep(IStep step)
        {
            return "autofunc_" + step.Name.Replace(" ", "_", StringComparison.InvariantCulture);
        }
    }
}
