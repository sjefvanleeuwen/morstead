using BlazorMonacoYaml;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.ApiCalls;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IEditorTabInfo : IYamlFileInfo
    {
        int TabId { get; set; }
        int OrderNr { get; set; }
        bool IsVisible { get; set; }
        bool IsActive { get; set; }
        bool HasChanges { get; }
        bool HasExceptions { get; }
        bool IsSaved { get; set; }
        string DisplayName { get; }
        string OriginalContent { get; set; }
        string Content { get; set; }
        string EditorId { get; }
        IEnumerable<FormattingException> Exceptions { get; set; }
        bool IsCompareMode { get; }
        IYamlFileInfo CompareInfo { get; set; }
        string CompareContent { get; set; }
        MonacoEditorYaml MonacoEditorYaml { get; set; }
        MonacoDiffEditorYaml MonacoDiffEditorYaml { get; set; }
        
        void RemoveExceptions();
    }
}