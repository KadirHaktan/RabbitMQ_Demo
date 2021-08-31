using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Shared.Services.Abstract;
using RabbitMQ.Shared.Services.Concerete;
using RabbitMQ.WebAPI.Configurations.Data.EfCore;
using RabbitMQ.WebAPI.Configurations.OData;
using RabbitMQ.WebAPI.Repositories;
using RabbitMQ.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI
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


            string connectionString = Configuration["Configurations:ConnectionString"];
            services.AddControllers();
            
          
            services.AddOData();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IMessageQueueService, RabbitMessageQueueService>();
            services.AddScoped<IProductRepository, EfCoreProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDbContext<NorthwindContext>(configuration =>
            {
                configuration.UseSqlServer(connectionString);
            });


            services.AddCors();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RabbitMQ.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ.WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapODataRoute("odata", "odata", ODataEntityConfigurations.EntityConfiguration().GetEdmModel());
                endpoints.MapControllers();
            });
        }
    }
}
