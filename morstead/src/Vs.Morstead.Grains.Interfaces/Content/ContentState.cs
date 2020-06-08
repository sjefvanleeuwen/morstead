using System.Net.Mime;
using System.Text;

namespace Vs.Morstead.Grains.Interfaces.Content
{
    public class ContentState
    {
        public byte[] Content { get; set; }
        public ContentType ContentType { get; set; }
        public long ContentLength { get; set; }
        public Encoding Encoding { get; set; }
        public CompressionType CompressionType { get; set; }
        public int ContentCompressedLength { get; set; }
    }
}
