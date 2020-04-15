using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class UrlBuilder
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        readonly ITextFormElementData YamlLogic = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Regels Yaml Url",
            Name = "Rule",
            Value = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml"
        };
        readonly ITextFormElementData YamlContent = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Content Yaml Url",
            Name = "Content",
            Value = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5Content.yaml"
        };

        private async Task Submit()
        {
            var pageBase = "/proefberekening/";
            var rules = "?rules=" + HttpUtility.UrlEncode(YamlLogic.Value);
            var content = "&content=" + HttpUtility.UrlEncode(YamlContent.Value);
            await OpenPage(pageBase + rules + content);
        }

        private async Task OpenPage(string url)
        {
            await JSRuntime.InvokeAsync<object>("open", url, "_blank");
        }
    }
}
