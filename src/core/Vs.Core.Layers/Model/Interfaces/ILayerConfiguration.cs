using Semver;
using System.Collections.Generic;

namespace Vs.Core.Layers.Model.Interfaces
{
    public interface ILayerConfiguration
    {
        IEnumerable<ILayer> Layers { get; set; }
        SemVersion Version { get; set; }
    }
}