using System;
using System.Collections.Generic;
using Vs.Core.Diagnostics;
using Vs.Core.Layers.Helpers;
using Vs.Core.Layers.Model;
using Vs.Core.Layers.Model.Interfaces;
using Vs.Rules.Routing.Model.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Rules.Routing.Model
{
    /// <summary>
    /// Contains the routing configuration for Virtual Society Systems
    /// </summary>
    public class RoutingConfiguration : IRoutingConfiguration, IYamlConvertible, IDebugInfo
    {
        //TODO stuurinformatie implementeren
        /// <summary>
        /// Gets or sets the stuurinformatie
        /// </summary>
        /// <value>
        /// The stuurinformation
        /// </value>
        public IGuidanceInformation Stuurinformatie { get; set; }
        /// <summary>
        /// Gets or sets the parameters
        /// </summary>
        /// <value>
        /// The parameters that have routing information.
        /// </value>
        public IEnumerable<RoutingParameter> Parameters { get; set; }

        private DebugInfo DebugInfo { get; set; }

        public LineInfo End => DebugInfo.End;

        public LineInfo Start => DebugInfo.Start;

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (TransformTemplate)nestedObjectDeserializer(typeof(TransformTemplate));
            if (o == null)
            {
                return;
            }
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);
            Stuurinformatie = o.stuurinformatie;
            var parameters = new List<RoutingParameter>();
            foreach (var parameter in o.parameters)
            {
                parameters.Add(new RoutingParameter
                {
                    Name = parameter.Name,
                    Location = parameter.Location
                });
            }
            Parameters = parameters;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new TransformTemplate(this));
        }

#pragma warning disable IDE1006 // Naming Styles
        private class TransformTemplate
        {
            public GuidanceInformation stuurinformatie { get; set; }

            public IEnumerable<RoutingParameter> parameters { get; set; } = new List<RoutingParameter>();

            public TransformTemplate()
            {
            }

            public TransformTemplate(IRoutingConfiguration local)
            {
                var paramList = new List<RoutingParameter>();
                //stuurinformatie = local.Stuurinformatie;
                foreach (var parameter in local.Parameters)
                {
                    paramList.Add(new RoutingParameter
                    {
                        Name = parameter.Name,
                        Location = parameter.Location
                    });
                }
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
