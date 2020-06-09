using System;
using Vs.Core.Layers.Model;

namespace Vs.Core.Layers.Controllers.Interfaces
{
    public interface ILayerController
    {
        LayerConfiguration LayerConfiguration { get; set; }

        void Initialize(string layerYaml);
        void Initialize(Uri layerUri);
    }
}