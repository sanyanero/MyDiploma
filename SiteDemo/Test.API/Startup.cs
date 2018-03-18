using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Test.MODELS;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Mvc.Authorization;
using Test.API.Helpers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Test.DAL.Abstract;
using Test.DAL.Concrete;
using AutoMapper;
using Test.MODELS.Options;
using Microsoft.EntityFrameworkCore;

namespace Test.API
{
    public class Startup
    {
        private const string TOKEN = "token";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase(Configuration.GetConnectionString("TestingConnectionString")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder

                    .WithOrigins("http://localhost:3000")
                    
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // Add framework services.
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper();

            services.AddMvc();

            ///Default JSON Serializer settings to handle looping references
            ///

            services.AddMvcCore().AddFormatterMappings().AddJsonFormatters()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            });
            // Add Database Initializer
            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDataBaseInitializer dbInitializer)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");

            app.UseExceptionHandler(
            options =>
            {
                options.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = "{ Error:" + $"{ ex.Error.Message}, ErrorTrace: {ex.Error.StackTrace }" + "}";
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });


            app.UseMvc();

            //Generate EF Core Seed Data
            ((DataBaseInitializer)dbInitializer).Initialize().Wait();

        }
    }
}
