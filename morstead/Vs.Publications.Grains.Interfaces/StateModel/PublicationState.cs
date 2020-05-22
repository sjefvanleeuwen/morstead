using Itenso.TimePeriod;
using System.Net.Mime;

namespace Vs.Publications.Grains.Interfaces.StateModel
{
    public class PublicationState
    {
        public object Content { get; set; }
        public ContentType ContentType {get;set;}
        public long ContentLength { get; set; }
    }
}
