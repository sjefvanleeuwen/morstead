using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors;
using System.Reflection;

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
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "FF";
                options.SubstituteApiVersionInUrl = true;
            });

            var doc = new OpenApiDocument();
            doc.Info.Title = "Virtual Society Rule Engine";
            doc.Info.License = new NSwag.OpenApiLicense() { Name = "MIT License", Url = "https://github.com/sjefvanleeuwen/virtual-society-urukagina/blob/master/LICENSE" };
            doc.Info.TermsOfService = "Dot not use in production.";
            //doc.Info.Description = "A Semantic Rule Engine API that plays nice with frontends.";
            doc.Info.Contact = new NSwag.OpenApiContact() { Url = "https://github.com/sjefvanleeuwen/virtual-society-urukagina/" };
            doc.Info.Description = "<img width=128 height=128 src='/img/logo.svg'></img><br/>A Semantic Rule Engine API that plays nice with frontends.</h1>";


            services
                .AddSwaggerDocument(document =>
                {
                    document.DocumentName = "2020-01-01.1.0";
                    document.ApiGroupNames = new[] { "2017-05-01.1.0" };
                    document.PostProcess = d =>
                    {
                        d.Info = doc.Info;
                        d.Info.Version = "2017-05-01.1.0";
                    };
                })
                .AddSwaggerDocument(document =>
                {
                    document.DocumentName = "2017-05-01.2.0-RC";
                    document.ApiGroupNames = new[] { "2017-05-01.2.0-RC" };
                    document.PostProcess = d =>
                    {
                        d.Info = doc.Info;
                        d.Info.Version = "2017-05-01.2.0-RC";
                    };
                });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
