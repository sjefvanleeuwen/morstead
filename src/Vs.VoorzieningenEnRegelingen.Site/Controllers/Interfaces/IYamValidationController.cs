using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.VoorzieningenEnRegelingen.Site.ApiCalls;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces
{
    public interface IYamlValidationController
    {
        IDictionary<YamlType, bool> Types { get; }
        bool GetEnabledForType(YamlType type);
        void CancelSubmitCountdown();
        Task<IEnumerable<FormattingException>> StartSubmitCountdown(string type, string yaml, int? overrideTimeOut = null);
        Task<IEnumerable<FormattingException>> SubmitPage(string type, string yaml);
    }
}