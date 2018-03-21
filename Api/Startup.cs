using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmileDirectClub.CodingTest.Infrastructure.Interfaces;
using SmileDirectClub.CodingTest.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SmileDirectClub.CodingTest.Api
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
            services.AddMvc();
            services.AddSingleton(new LoggerFactory().AddConsole().AddDebug());
            services.AddLogging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Launchpad API", Version = "v1", Description = "A .NET Core 2.0 API for launchpad information" });
                string basePath = AppContext.BaseDirectory;
                string xmlPath = Path.Combine(basePath, "SmileDirectClub.CodingTest.Api.xml");
                c.IncludeXmlComments(xmlPath);

            });
            services.AddScoped<ILaunchPadRepository, LaunchpadApiRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(); 

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Launchpad API v1");
            });

            app.UseMvc();
        }
    }
}
