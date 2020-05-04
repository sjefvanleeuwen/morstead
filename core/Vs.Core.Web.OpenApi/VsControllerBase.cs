using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;

namespace Vs.Core.Web.OpenApi
{
    public class VsControllerBase : ControllerBase
    {
        public async Task<ObjectResult> Download(Uri endpoint)
        {
            string yaml;
            try
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        yaml = client.DownloadString(endpoint.AbsoluteUri);
                    }
                    catch (WebException ex)
                    {
                        return StatusCode(404, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex));
            }
            return StatusCode(200, yaml);
        }
    }
}
