using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NSwag;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using App.Metrics;

namespace Vs.Core.Web.OpenApi.v1.Middleware
{
    public static class ConfigureOpenApi
    {
        public static void UseOpenApiStrategy(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMetricsActiveRequestMiddleware();
            app.UseMetricsErrorTrackingMiddleware();
            app.UseMetricsPostAndPutSizeTrackingMiddleware();
            app.UseMetricsRequestTrackingMiddleware();
            app.UseMetricsOAuth2TrackingMiddleware();
            app.UseMetricsApdexTrackingMiddleware();

            app.UseMetricsAllEndpoints();
        }

        public static void AddDocument(this IServiceCollection serviceCollection, Action<ApiDocument> configure)
        {
            var apiDocument = new ApiDocument();
            configure(apiDocument);
            
            OpenApiDocument d = new OpenApiDocument();
            d.Info = new OpenApiInfo();
            d.Info.Title = apiDocument.Title;
            d.Info.Description = apiDocument.Description;
            d.Info.Contact = new OpenApiContact();
            d.Info.Contact.Email = "innovatie@wigo4it.nl";
            d.Info.Contact.Name = "I&O&UX";
            d.Info.License = new OpenApiLicense();
            d.Info.License.Name = "MIT";
            d.Info.License.Url = "https://github.com/sjefvanleeuwen/virtual-society-urukagina/blob/master/LICENSE";
            d.Info.TermsOfService = "Prototype. Do not use in production";
            d.Info.Version = apiDocument.Version;
            serviceCollection.AddSwaggerDocument(document =>
            {
                document.DocumentName = apiDocument.Name;
                document.ApiGroupNames = apiDocument.ApiGroupNames;
                document.PostProcess = doc =>
                {
                    doc.Generator = "Virtual Society";
                    doc.Info = d.Info;
                    doc.SchemaType = NJsonSchema.SchemaType.OpenApi3;
                };
            });
        }

        public static void AddOpenApiServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandling = ReferenceHandling.Preserve;
                }
            );
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddDocument(document =>
            {
                document.Name = "1.0-core";
                document.ApiGroupNames = new[] { "1.0-core" };
                document.Description = @"<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.

<h2>What you need to know about our API versioning strategy</h2>

Virtual Society Releases its public API's using major versioning with feature requests in each version.

<ul>
    <li>A Maximum of 2 Major versions will always be available</li>
    <li>New feature requests will be maintained in isolated url's for you to try out</li>
    <li>Feature requests should be considered Beta and will be merged into the Next Major version when tested thoroughly</li>
    <li>The previous major version will be phased out as soon as the third major version comes online.</li>
    <li>We will maintain a cooldown period between major version updates so you can upgrade</li>
</ul>

<h2>What you need to know about our API core contract</h2>
<ul>
    <li>Contains generic capabilities for TypeScript Client Code Generation</li>
    <li>Contains generic capabilities for CSharp Client Code Generation</li>
    <li>Contains default protocol response messages that are implemented by all API's:</li>
    <ul>
        <li>NotFound404Response</li>
        <li>Server500Response</li>
        <li>SwaggerContractErrorResponse</li>
    </ul>
</ul>

<h2> API Metrics Endpoints</h2>

This API provides three endpoints for metrics. Metric snapshots can be exposed over HTTP in different formats as well as information about the running environment of the application

<ul>
    <li><a href='/metrics'>/metrics</a> Exposes a metrics snapshot using the configured metrics formatter.</li>
    <li><a href='/metrics-text'>/metrics-text</a> Exposes a metrics snapshot using the configured text formatter.</li>
    <li><a href='/env'>/env</a> Exposes environment information about the application e.g. OS, Machine Name, Assembly Name, Assembly Version etc.</i>
</ul>
";
                     document.Title = "Virtual Society Open Api Strategy";
                     document.Version = "1.0-core";
                 });

            var metrics = AppMetrics.CreateDefaultBuilder()
            .Build();

            services.AddMetrics(metrics);
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsEndpoints();

        }
    }
}
