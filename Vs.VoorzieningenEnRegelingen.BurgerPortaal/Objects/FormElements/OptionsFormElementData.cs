﻿using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class OptionsFormElementData : FormElementData, IOptionsFormElementData
    {
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        public override void FillFromExecutionResult(IExecutionResult result)
        {
            base.FillFromExecutionResult(result);

            Options = FormElementHelper.DefineOptions(result);
        }
    }
}
