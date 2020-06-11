using BlazorMonaco.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers.ApiCalls;

namespace Vs.YamlEditor.Components.Controllers
{
    public class ValidationController
    {
        private CancellationTokenSource TokenSource { get; set; }
        public Shared.YamlEditor YamlEditor { get; set; }
        public string TypeOfContent { get; set; }

        private readonly TimeSpan _submitWait = TimeSpan.FromMilliseconds(5000);

        private IDictionary<YamlType, bool> _typesDefinitions;

        public IDictionary<YamlType, bool> Types => GetTypeDefinitions();

        private IDictionary<YamlType, bool> GetTypeDefinitions()
        {
            if (_typesDefinitions != null)
            {
                return _typesDefinitions;
            }

            _typesDefinitions = new Dictionary<YamlType, bool>();

            var availableValidation = new List<YamlType> { YamlType.Rules };

            foreach (var yamlType in (YamlType[])Enum.GetValues(typeof(YamlType)))
            {
                _typesDefinitions.Add(yamlType, availableValidation.Contains(yamlType));
            }
            return _typesDefinitions;
        }

        public IDictionary<YamlType, bool> DisabledTypes => GetTypeDefinitions().Where(t => !t.Value).ToDictionary(t => t.Key, t => t.Value);

        public bool GetEnabledForType(YamlType type)
        {
            return Types.ContainsKey(type) && Types[type];
        }

        public void StartSubmitCountdown()
        {
            if (TokenSource != null)
            {
                TokenSource.Cancel();
            }
            TokenSource = new CancellationTokenSource();
            var ct = TokenSource.Token;
            var task = Task.Run(() =>
            {
                Thread.Sleep(_submitWait);
                ct.ThrowIfCancellationRequested();
                SubmitPage();
            },
            TokenSource.Token);
        }

        public async void SubmitPage()
        {
            if (string.IsNullOrWhiteSpace(TypeOfContent))
            {
                return;
            }

            var client = new RulesControllerDisciplClient(new HttpClient())
            {
                BaseUrl = "https://localhost:44391/"
            };

            //DebugRuleYamlFromContentResponse response = null;
            IEnumerable<FormattingException> formattingExceptions = new List<FormattingException>();
            try
            {
                var response = await client.DebugRuleYamlContentsAsync(new DebugRuleYamlFromContentRequest { Yaml = await YamlEditor.GetValue() });
                formattingExceptions = response.ParseResult.FormattingExceptions;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode != 404)
                {
                    throw ex;
                }
                await YamlEditor.ResetDeltaDecorations();
                return;
            }

            await SetDeltaDecorationsFromExceptions(formattingExceptions);
        }

        public async Task SetDeltaDecorationsFromExceptions(IEnumerable<FormattingException> formattingExceptions)
        {
            await YamlEditor.ResetDeltaDecorations();

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

                deltaDecorations.Add(await BuildDeltaDecoration(range, message));
            }

            await YamlEditor.SetDeltaDecoration(deltaDecorations);
        }

        public async Task<ModelDeltaDecoration> BuildDeltaDecoration(BlazorMonaco.Bridge.Range range, string message)
        {
            var isWholeLine = false;

            range.StartLineNumber = Math.Max(range.StartLineNumber, 1);
            range.StartColumn = Math.Max(range.StartColumn, 1);
            range.EndLineNumber = Math.Max(range.EndLineNumber, 1);
            if (range.EndColumn == 0)
            {
                range.EndColumn = range.StartColumn;
                var content = await YamlEditor.GetValue();
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
                GlyphMarginClassName = "editorErrorGlyph",
                GlyphMarginHoverMessage = new MarkdownString[] { new MarkdownString { Value = $"**Error**\r\n\r\n{message}" } }
            };

            return new ModelDeltaDecoration { Range = range, Options = options };
        }
    }
}
