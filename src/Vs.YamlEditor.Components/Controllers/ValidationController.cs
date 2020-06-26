using BlazorMonaco.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers.ApiCalls;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components.Controllers
{
    public class ValidationController : IValidationController
    {
        private CancellationTokenSource TokenSource { get; set; }

        private readonly TimeSpan _submitWait = TimeSpan.FromMilliseconds(2000);

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

        public bool GetEnabledForType(YamlType type)
        {
            return Types.ContainsKey(type) && Types[type];
        }

        public async Task<IEnumerable<FormattingException>> StartSubmitCountdown(string type, string yaml)
        {
            Task<IEnumerable<FormattingException>> result = null;
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
                result = SubmitPage(type, yaml);
            },
            TokenSource.Token);
            if (result == null)
            {
                return null;
            }

            return await result;
        }

        public async Task<IEnumerable<FormattingException>> SubmitPage(string type, string yaml)
        {
            if (type == YamlType.Rules.GetDescription())
            {
                return await RuleValidation(yaml);
            }

            return null;
        }

        private async Task<IEnumerable<FormattingException>> RuleValidation(string yaml)
        {
            var client = new RulesControllerDisciplClient(new HttpClient())
            {
                BaseUrl = "https://localhost:44391/"
            };

            IEnumerable<FormattingException> formattingExceptions;
            try
            {
                var response = await client.DebugRuleYamlContentsAsync(new DebugRuleYamlFromContentRequest { Yaml = yaml });
                return formattingExceptions = response.ParseResult.FormattingExceptions;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode != 404)
                {
                    throw ex;
                }

                return null;
            }
        }
    }
}
