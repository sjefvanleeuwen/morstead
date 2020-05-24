using Itenso.TimePeriod;
using System.Net.Mime;
using System.Text;

namespace Vs.Publications.Grains.Interfaces.StateModel
{
    public class PublicationState
    {
        public byte[] Content { get; set; }
        public ContentType ContentType {get;set;}
        public long ContentLength { get; set; }
        public Encoding Encoding { get; set; }
        public CompressionType CompressionType { get; set; }
        public int ContentCompressedLength { get; set; }
    }
}
