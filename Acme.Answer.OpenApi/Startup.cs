using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vs.Core.Web.OpenApi.v1.Middleware;

namespace Acme.Answer.OpenApi
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

            //set up the default document information
            var doc = new ApiDocument();
            doc.Title = "Virtual Society Rule Engine";
            doc.Description = @"

<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.

<h2>What you need to know about our API Versioning strategy</h2>

Virtual Society Releases its public API's using versioning.

<h3> Format </h3>

We specify a Full API version with optional minor version as [group version][.major[.minor,0]][-status]

<i>For Example:</i>

2017-05-01.1-alpha
2018-05-01.1-beta
2018-05-01.1-rc
2018-05-01.1-release

<h4>Alpha releases</h4>

Alpha releases are a work in progress, open for feedback and improvements and not guaranteed to work.
an alpha version can become obsolete or removed at any given time without prior notice.

<h4>Beta releases</h4>

Beta releases are a work in progress, open for feedback and improvements and, though not guaranteed should work in most known scenarios.
a beta version can become increase its version number without prior notice, but might be published for people who are actively involved with us in a beta program.

<h4>RC (or release candidate) releases</h4>

RC releases are a work in progress, open for feedback and improvements and, should work should work in all known scenarios. The RC will not accept new functionalities.
Feedback might not be accepted and move to another version release in the future. RC's are publicly announced on the project site as specified in this OpenAPI specification.

<h4>Final releases</h4>

Final releases should work in all scenario's. Might issues arise patches might be release using an increment in the minor version.
";

            //add specific versions
            services.AddDocument(document =>
            {
                document.Title = doc.Title;
                document.Description = doc.Description;
                document.Name = "2.0";
                document.ApiGroupNames = new[] { "2" };
                document.Version = "2.0";
            });

            services.AddDocument(document =>
            {
                document.Title = doc.Title;
                document.Description = @"
<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.

<h2>What you need to know about this feature branch</h2>

This feature branche allows you to quickly use new features as they are requested. We will eventually make a major increment version and migrate the features so they place nice together in a next alpha/beta release.

Eventually the seperate features will become obsolete, we allow for a cooldown period so you can upgrade to the new version. You can join us in testing the alpha/beta releases or migrate over once the next major version reaches 
RC / Release status.";
                document.Name = "2.0-features";
                document.ApiGroupNames = new[] { "2.0-feature1", "2.0-feature2" };
                document.Version = "2.0-features";

            });

            services.AddDocument(document =>
            {
                document.Name = "1.0-release";
                document.ApiGroupNames = new[] { "1" };
                document.Version = "1.0-release";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApiStrategy(env);
        }
    }
}
