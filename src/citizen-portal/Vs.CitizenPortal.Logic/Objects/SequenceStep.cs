﻿using System.Collections.Generic;
using System.Linq;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.CitizenPortal.Logic.Objects
{
    public class SequenceStep : ISequenceStep
    {
        public int Key { get; set; }
        public string SemanticKey { get; set; }
        public string ParameterName { get; set; }
        public IEnumerable<string> ValidParameterNames { get; set; }

        public bool HasMultipleValidParameterNames() => ValidParameterNames?.ToList()?.Any() ?? false;

        public bool IsMatch(IParameter parameter)
        {
            return parameter != null &&

                    (!(parameter is IClientParameter) || SemanticKey == parameter.SemanticKey) &&
                    (
                        ParameterName == parameter.Name ||
                        ValidParameterNames != null && ValidParameterNames.Contains(parameter.Name)
                    )
                ;
        }
    }
}
