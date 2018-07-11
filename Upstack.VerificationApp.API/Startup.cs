using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Upstack.VerificationApp.API.Contracts;
using Upstack.VerificationApp.API.Entities;
using Upstack.VerificationApp.API.Filter;
using Upstack.VerificationApp.API.Services;
using AutoMapper;
using Upstack.VerificationApp.API.Model;

namespace Upstack.VerificationApp.API
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
            services.AddDbContext<APIDbContext>(opt =>
             opt.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddMvc();
            // configure global exception tracker
            services.AddMvc(opt => opt.Filters.Add(typeof(JsonExceptionFilter)));
            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.Configure<SendGridModel>(Configuration.GetSection("SendGrid"));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                     builder =>
                     {
                         builder.AllowAnyOrigin();
                     });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseStaticFiles();
            Mapper.Initialize(opt =>
            {
                opt.CreateMap<UserBindingModel, UserModel>();
            });
        }
    }
}
