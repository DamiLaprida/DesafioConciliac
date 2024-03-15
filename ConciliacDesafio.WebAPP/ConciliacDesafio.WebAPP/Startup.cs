using AutoMapper;
using ConciliacDesafio.Domain.Contracts.Repository;
using ConciliacDesafio.Domain.Contracts.Services;
using ConciliacDesafio.Domain.Services;
using ConciliacDesafio.Persistence.Context;
using ConciliacDesafio.Persistence.MappingProfile;
using ConciliacDesafio.Persistence.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

namespace ConciliacDesafio.WebAPP
{
    public class Startup
    {
        private readonly ILogger _logger;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var tareasDbConnectionString = Configuration.GetConnectionString("TareasDB");
            services.AddDbContext<TareasContext>(option => option.UseSqlServer(tareasDbConnectionString));
            services.AddTransient<ITareaRepository, TareaRepository>();
            services.AddTransient<ITareaService, TareaService>();
            services.AddHealthChecks();

            //Configuración de automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TareasWebApp", Version = "v1" }));
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TareasWebApp v1"));
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        _logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync("Internal Server Error.");
                    }
                });
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("api/healthcheck");
            });
        }
    }
}
