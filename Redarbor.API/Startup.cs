using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redarbor.API.Profiles;
//using Microsoft.Extensions.Logging;
using Redarbor.DAL.Implementations;
using Redarbor.DAL.Interfaces;
using Redarbor.Library.Implementations;
using Redarbor.Library.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redarbor.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Serilog
            services.AddSingleton((ILogger)new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("C:\\Logs\\log.txt")
            .CreateLogger());

            //AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });            
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Autofac
            var containerBuilder = RegisterTypes(services);

            return new AutofacServiceProvider(containerBuilder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IContainer RegisterTypes(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EmployeeLibrary>().As<IEmployeeLibrary>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();

            builder.Populate(services);

            return builder.Build();
        }
    }
}
