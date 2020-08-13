﻿using System.Collections.Generic;
using System.Linq;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.DataModel.Model.FormElements
{
    public class ListFormElementData : OptionsFormElementData, IListFormElementData
    {
        public override string Value
        {
            get
            {
                if (value == string.Empty)
                {
                    return Options.ToList().FirstOrDefault().Key;
                }
                return value;
            }
            set { base.value = value; }
        }

        public override void DefineOptions(IExecutionResult result, IContentController contentController)
        {
            if (result?.QuestionFirstParameter?.Value == null)
            {
                return;
            }
            foreach (var item in result.QuestionFirstParameter.Value as IEnumerable<object>)
            {
                var itemString = item.ToString();
                Options.Add(itemString, itemString.Substring(0, 1).ToUpper() + itemString.Substring(1));
            }
        }
    }
}
