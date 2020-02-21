using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class Result : IResult
    {
        public string Description { get; set; }
        public string ResultTemplate { get; set; }
        public Dictionary<string, string> TemplateVariables { get; set; }
    }
}
