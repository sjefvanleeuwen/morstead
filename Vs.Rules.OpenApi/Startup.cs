using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using System.Text.Json.Serialization;
using Vs.Core.Web.OpenApi.Middleware;

namespace Vs.Rules.OpenApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenApiServices();

            var doc = new OpenApiDocument();
            doc.Info.Title = "Virtual Society Rule Engine";
            doc.Info.License = new NSwag.OpenApiLicense() { Name = "MIT License", Url = "https://github.com/sjefvanleeuwen/virtual-society-urukagina/blob/master/LICENSE" };
            doc.Info.TermsOfService = "Dot not use in production.";
            //doc.Info.Description = "A Semantic Rule Engine API that plays nice with frontends.";
            doc.Info.Contact = new NSwag.OpenApiContact() { Url = "https://github.com/sjefvanleeuwen/virtual-society-urukagina/" };
            doc.Info.Description = @"

<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.

<h2>What you need to know about our API Versioning strategy</h2>

Virtual Society Releases its public API's using major versioning with feature requests in each version.

<ul>
    <li>A Maximum of 2 Major versions will always be available</li>
    <li>New feature requests will be maintained in isolated url's for you to try out</li>
    <li>Feature requests should be considered Beta and will be merged into the Next Major version when tested thoroughly</li>
    <li>The previous major version will be phased out as soon as the third major version comes online.</li>
    <li>We will maintain a cooldown period between major version updates so you can upgrade</li>
</ul>
";

            services
                 .AddSwaggerDocument(document =>
                 {
                     document.DocumentName = "1.0";
                     document.ApiGroupNames = new[] { "1" };
                     document.PostProcess = d =>
                     {
                         d.Info = doc.Info;
                         d.Info.Version = "1.0";
                     };
                 })
                .AddSwaggerDocument(document =>
                {
                    document.DocumentName = "1.0-features";
                    document.ApiGroupNames = new[] { "1.0-discipl" };
                    document.PostProcess = d =>
                    {
                        d.Info = doc.Info;
                        d.Info.Version = "1.0-features";
                        d.Info.Description = @"
<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.

<h2>What you need to know about this feature branch</h2>

This feature branche allows you to quickly use new features as they are requested. We will eventually make a major increment version and migrate the features so they place nice together in a next alpha/beta release.

Eventually the separate features will become obsolete, we allow for a cooldown period so you can upgrade to the new version. You can join us in testing the alpha/beta releases or migrate over once the next major version reaches 
RC / Release status.
";
                    };
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApiStrategy(env);
        }
    }
}
