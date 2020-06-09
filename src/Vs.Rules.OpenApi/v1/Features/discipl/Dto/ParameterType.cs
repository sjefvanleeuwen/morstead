using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Parameter type enumerations
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ParameterType
    {
        /// <summary>
        /// Double type for numbers, decimals, currency etc.
        /// </summary>
        Double = 1,
        /// <summary>
        /// Text field
        /// </summary>
        String = 4,
        /// <summary>
        /// Boolean field
        /// </summary>
        Boolean = 5,
        /// <summary>
        /// List
        /// </summary>
        List = 6,
    }
}
