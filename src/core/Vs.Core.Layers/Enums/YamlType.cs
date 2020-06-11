using System.ComponentModel;

namespace Vs.Core.Layers.Enums
{
    public enum YamlType
    {
        [Description("Layer")]
        Layer,
        [Description("Rule")]
        Rules,
        [Description("Content")]
        Uxcontent,
        [Description("Routing")]
        Routing
    }
}
