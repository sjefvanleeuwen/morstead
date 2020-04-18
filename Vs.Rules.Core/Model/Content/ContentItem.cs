using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Core.Diagnostics;
using Vs.Core.Semantic;
using Vs.Core.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.Content
{
    public class ContentItem : IContentItem, ISerialize, ISemanticKey
    {
        public string SemanticKey { get; set; }

        public DebugInfo DebugInfo { get; set; }
        IEnumerable<KeyValuePair<CultureInfo, string>> IContentItem.Body { get; set; }

        private class DeserializeTemplate : IContentItem, ISemanticKey
        {
            public string Type { get; set; }
            public IEnumerable<KeyValuePair<CultureInfo, string>> Body { get; set; }
            public string SemanticKey { get; set; }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            throw new NotImplementedException();
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            throw new NotImplementedException();
        }
    }
}
