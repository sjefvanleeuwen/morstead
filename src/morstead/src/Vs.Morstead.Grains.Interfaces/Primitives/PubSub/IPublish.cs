using System;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Primitives.PubSub
{
    public interface IPublish
    {
        Task Notify(string topic);
    }
}
