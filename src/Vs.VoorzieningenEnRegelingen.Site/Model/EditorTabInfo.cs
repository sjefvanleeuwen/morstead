using BlazorMonacoYaml;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.ApiCalls;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class EditorTabInfo : YamlFileInfo, IEditorTabInfo
    {
        public int TabId { get; set; }
        public int OrderNr { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
        public bool HasChanges => Content != OriginalContent;
        public bool HasExceptions => Exceptions?.Any() ?? false;
        public bool IsSaved { get; set; }
        public string DisplayName => (string.IsNullOrWhiteSpace(Name) ? "<i>Nieuw</i>" : Name) +
            (HasChanges && !IsSaved ? "*" : string.Empty) +
            (IsCompareMode ? " <i class=\"fa fa-exchange\"></i> " + " <i class=\"fa fa-check\"></i> " + CompareInfo.Name : string.Empty);
        public string OriginalContent { get; set; }
        public string Content { get; set; }
        public string EditorId => "monacoEditor" + TabId;
        public IEnumerable<FormattingException> Exceptions { get; set; }
        public bool IsCompareMode => CompareInfo?.ContentId != null;
        public IYamlFileInfo CompareInfo { get; set; }
        public string CompareContent { get; set; }
        public MonacoEditorYaml MonacoEditorYaml { get; set; }
        public MonacoDiffEditorYaml MonacoDiffEditorYaml { get; set; }

        public void RemoveExceptions()
        {
            Exceptions = null;
        }
    }
}
