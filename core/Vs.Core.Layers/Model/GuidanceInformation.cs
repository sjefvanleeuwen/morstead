using Semver;
using System;
using Vs.Core.Diagnostics;
using Vs.Core.Layers.Helpers;
using Vs.Core.Layers.Model.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Contains guidance information for Virtual Society Systems
    /// </summary>
    public class GuidanceInformation : IGuidanceInformation, IYamlConvertible, IDebugInfo
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the organisation.
        /// </summary>
        /// <value>
        /// The organisation.
        /// </value>
        public string Organisation { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain { get; set; }
        /// <summary>
        /// Gets or sets the semantic version.
        /// </summary>
        /// <value>
        /// The semantic version.
        /// </value>
        public SemVersion Version { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

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

            Subject = o.onderwerp;
            Organisation = o.organisatie;
            Type = o.type;
            Domain = o.domein;
            Version = o.versie;
            Status = o.status;
            Year = o.jaar;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new TransformTemplate(this));
        }

#pragma warning disable IDE1006 // Naming Styles
        private class TransformTemplate
        {
            public string onderwerp { get; set; }
            public string organisatie { get; set; }
            public string type { get; set; }
            public string domein { get; set; }
            public SemVersion versie { get; set; }
            public string status { get; set; }
            public int jaar { get; set; }

            public TransformTemplate()
            {
            }

            public TransformTemplate(IGuidanceInformation local)
            {
                onderwerp = local.Subject;
                organisatie = local.Organisation;
                type = local.Type;
                domein = local.Domain;
                versie = local.Version;
                status = local.Status;
                jaar = local.Year;
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
