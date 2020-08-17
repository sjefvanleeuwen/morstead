using System;
using System.Threading.Tasks;
using Vs.Core.Layers.Model;

namespace Vs.Core.Layers.Controllers.Interfaces
{
    public interface ILayerController
    {
        LayerConfiguration LayerConfiguration { get; set; }

        Task Initialize(string layerYaml);
        Task Initialize(Uri layerUri);
    }
}