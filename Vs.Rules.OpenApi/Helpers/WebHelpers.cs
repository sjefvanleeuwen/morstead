using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Vs.Rules.OpenApi.Helpers
{
    public static class WebHelpers
    {
        /// <summary>
        /// Downloads the yaml.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        public static YamlDownloadResult DownloadYaml(this Uri endpoint)
        {
            var result = new YamlDownloadResult()
            {
                Endpoint = endpoint,
                StatusCode = HttpStatusCode.OK
            };

            using (var client = new WebClient())
            {
                try
                {
                    result.Content = client.DownloadString(endpoint);
                    result.StatusCode = HttpStatusCode.OK;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            result.StatusCode = response.StatusCode;
                        }
                        else
                        {
                            result.StatusCode = null;
                        }
                    }
                    result.WebException = ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Contains the result of a download request
        /// </summary>
        public class YamlDownloadResult
        {
            public Uri Endpoint { get; set; }
            public HttpStatusCode? StatusCode { get; set; }
            public WebException? WebException { get; set; }
            public string? Content { get; set; }
        }
    }
}
