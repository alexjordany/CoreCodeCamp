using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreCodeCamp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CampContext>();
            services.AddScoped<ICampRepository, CampRepository>();
            services.AddAutoMapper(typeof (Startup));
            services.AddApiVersioning(opt => {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 1);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new QueryStringApiVersionReader("X-Version");
            });
            services.AddControllers(opt => opt.EnableEndpointRouting=false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
