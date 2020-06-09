using System.Collections.Generic;

namespace Vs.Core.Layers.Model.Interfaces
{
    public interface ILayer
    {
        string Name { get; set; }
        IEnumerable<IContext> Contexts { get; set; }
    }
}