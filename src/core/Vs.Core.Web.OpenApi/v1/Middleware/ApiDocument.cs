using System;

namespace Vs.Core.Web.OpenApi.v1.Middleware
{
    /// <summary>
    /// Used for describing the openapi document.
    /// </summary>
    public class ApiDocument
    {
        public string Name { get; set; }
        public string[] ApiGroupNames { get; set;}
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ApiDocument() { }

    }
}
