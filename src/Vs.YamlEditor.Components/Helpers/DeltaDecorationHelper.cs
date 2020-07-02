using BlazorMonaco.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.YamlEditor.Components.Controllers.ApiCalls;

namespace Vs.YamlEditor.Components.Helpers
{
    public static class DeltaDecorationHelper
    {
        public static async Task SetDeltaDecorationsFromExceptions(Shared.YamlEditor yamlEditor, IEnumerable<FormattingException> formattingExceptions)
        {
            if (formattingExceptions == null)
            {
                return;
            }
            await yamlEditor.ResetDeltaDecorations();

            var deltaDecorations = new List<ModelDeltaDecoration>();
            foreach (var exception in formattingExceptions)
            {
                var message = exception.Message;
                var range = new BlazorMonaco.Bridge.Range()
                {
                    StartLineNumber = exception.DebugInfo.Start.Line,
                    StartColumn = exception.DebugInfo.Start.Col,
                    EndLineNumber = exception.DebugInfo.End.Line,
                    EndColumn = exception.DebugInfo.End.Col
                };

                deltaDecorations.Add(await BuildDeltaDecoration(yamlEditor, range, message));
            }

            await yamlEditor.SetDeltaDecoration(deltaDecorations);
        }

        private static async Task<ModelDeltaDecoration> BuildDeltaDecoration(Shared.YamlEditor yamlEditor, BlazorMonaco.Bridge.Range range, string message)
        {
            var isWholeLine = false;

            range.StartLineNumber = Math.Max(range.StartLineNumber, 1);
            range.StartColumn = Math.Max(range.StartColumn, 1);
            range.EndLineNumber = Math.Max(range.EndLineNumber, 1);
            if (range.EndColumn == 0)
            {
                range.EndColumn = range.StartColumn;
                var content = await yamlEditor.GetValue();
                var contentLines = content.Split("\n");
                range.EndColumn = (contentLines.ElementAt(Math.Min(contentLines.Length - 1, range.EndLineNumber - 1))?.Trim().Length ?? 0) + 1;
                isWholeLine = true;
            }

            var options = new ModelDecorationOptions
            {
                IsWholeLine = isWholeLine,
                InlineClassName = "editorError",
                InlineClassNameAffectsLetterSpacing = false,
                ClassName = "editorError",
                HoverMessage = new MarkdownString[] { new MarkdownString { Value = $"**Error**\r\n\r\n{message}" } },
                GlyphMarginClassName = "editorErrorGlyph fa fa-exclamation-circle",
                GlyphMarginHoverMessage = new MarkdownString[] { new MarkdownString { Value = $"**Error**\r\n\r\n{message}" } }
            };

            return new ModelDeltaDecoration { Range = range, Options = options };
        }
    }
}
