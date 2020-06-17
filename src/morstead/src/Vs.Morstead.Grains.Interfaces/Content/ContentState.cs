using System;
using System.Net.Mime;
using System.Text;

namespace Vs.Morstead.Grains.Interfaces.Content
{
    [Serializable]
    public class ContentState
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public EncodingType Encoding { get; set; }
        public CompressionType CompressionType { get; set; }
        public int ContentCompressedLength { get; set; }

        public string ContentAs<T>()
        {
            switch (Type.GetTypeCode(typeof(T))) {
                case TypeCode.String:
                    return GetEncoding().GetString(Content);
            }
            throw new Exception($"Casting content to {Type.GetTypeCode(typeof(T))} is not supported ");
        }

        public Encoding GetEncoding()
        {
            switch (Encoding)
            {
                case EncodingType.UTF8:
                    return System.Text.Encoding.UTF8;
                default:
                    throw new Exception("Unsupported Encoding Type");
            }
        }
    }
}
