using Application;
using Application.Cars;
using Application.Contracts;
using Application.Services;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Models.ConexionDataBase;
using MongoDB.Driver;
using System;
using System.IO;
using System.Reflection;


namespace API
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración de MongoDB
            services.Configure<MongoConexion>(Configuration.GetSection("MongoDB"));
            services.AddSingleton(s => new MongoClient(Configuration.GetValue<string>("MongoDB:ConexionChain")));

            // Otros servicios y configuraciones 
            services.AddRepositories();

            services.AddMediatR(typeof(AddCars.Query).Assembly);
            services.AddMediatR(typeof(GetCars.Query).Assembly);

            services.AddAutoMapper(typeof(AddCars.Query));
            services.AddSingleton<IGeolocationService, GeolocationService>();
            services.AddSingleton<IDefinedService, DefinedService>();
            services.AddMediatR(typeof(Startup).Assembly); 


            // Configuración de Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tu API", Version = "v1" });
            });

            // Configuración de controllers y otras configuraciones
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuración de Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Renta Car v1"));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}


