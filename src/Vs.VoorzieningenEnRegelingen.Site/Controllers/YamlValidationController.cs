using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;
using Vs.VoorzieningenEnRegelingen.Site.ApiCalls;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers
{
    public class YamlValidationController : IYamlValidationController
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

        public void CancelSubmitCountdown()
        {
            TokenSource.Cancel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="yaml"></param>
        /// <returns>An (empty) list of all formatexceptions or null in case of a non-result</returns>
        public async Task<IEnumerable<FormattingException>> StartSubmitCountdown(string type, string yaml, int? overrideTimeOut = null)
        {
            var timeOut = _submitWait;
            if (overrideTimeOut != null)
            {
                timeOut = TimeSpan.FromMilliseconds(overrideTimeOut.Value);
            }

            Task<IEnumerable<FormattingException>> result = null;
            if (TokenSource != null)
            {
                TokenSource.Cancel();
            }
            TokenSource = new CancellationTokenSource();
            var ct = TokenSource.Token;
            try
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(timeOut);
                    ct.ThrowIfCancellationRequested();
                    result = SubmitPage(type, yaml);
                },
                TokenSource.Token).ConfigureAwait(false);
            }
            catch
            {
                //do nothing
            }

            if (result == null)
            {
                return null;
            }

            return await result.ConfigureAwait(false);
        }

        public async Task<IEnumerable<FormattingException>> SubmitPage(string type, string yaml)
        {
            if (yaml == null || type == YamlType.Rules.GetDescription())
            {
                return await RuleValidation(yaml).ConfigureAwait(false);
            }

            return null;
        }

        private static async Task<IEnumerable<FormattingException>> RuleValidation(string yaml)
        {
            var client = new RulesControllerDisciplClient(new HttpClient())
            {
                BaseUrl = "https://localhost:44391/"
            };

            IEnumerable<FormattingException> formattingExceptions;
            try
            {
                var response = await client.DebugRuleYamlContentsAsync(new DebugRuleYamlFromContentRequest { Yaml = yaml }).ConfigureAwait(false);
                return formattingExceptions = response.ParseResult.FormattingExceptions;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode != 404)
                {
                    throw;
                }

                return new List<FormattingException>();
            }
        }
    }
}
