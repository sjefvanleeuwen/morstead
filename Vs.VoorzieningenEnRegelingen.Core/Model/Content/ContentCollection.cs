using System;
using System.Collections.Generic;
using Vs.Graph.Core.Data;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.Content
{
    public class ContentCollection : List<ContentItem>, ISerialize
    {
        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (dynamic)nestedObjectDeserializer(typeof(object));
            foreach (var item in o["content"])
            {
                this.Add(new ContentItem() { SemanticKey = item[""] });
            }
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            throw new NotImplementedException();
        }
    }
}
