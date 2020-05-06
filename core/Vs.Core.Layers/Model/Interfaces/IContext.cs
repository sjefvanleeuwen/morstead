using Nager.Country;
using System;

namespace Vs.Core.Layers.Model.Interfaces
{
    public interface IContext
    {
        Uri Endpoint { get; set; }
        LanguageCode? Language { get; set; }
    }
}