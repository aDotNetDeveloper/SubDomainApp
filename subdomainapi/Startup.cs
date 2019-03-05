using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SubDomain.Repositories;

namespace SubDomain
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().WithMethods(new string[] { "POST", "GET" }).WithHeaders(HeaderNames.ContentType, "application/json"));
            });

            services.AddMvc();

            // repositories
            services.AddSingleton<IDNSProvider, DNSProvider>();
            services.AddTransient<IDomainService, DomainService>();
            services.AddTransient<IIPAddressFinder, IPAddressFinder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // we are an open api
            app.UseCors("AllowAllOrigins");

            app.UseMvcWithDefaultRoute();
        }
    }
}
