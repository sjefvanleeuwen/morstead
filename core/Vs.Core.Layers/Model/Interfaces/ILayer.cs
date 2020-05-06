using System.Collections.Generic;

namespace Vs.Core.Layers.Model.Interfaces
{
    public interface ILayer
    {
        IEnumerable<IContext> Contexts { get; set; }
        string Name { get; set; }
    }
}