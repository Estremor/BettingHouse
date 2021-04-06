using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingHouse.Domain;
using BettingHouse.Model;
using BettingHouse.Repository;
using EasyCaching.Core.Configurations;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BettingHouse
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
            services.AddEasyCaching(options =>
            {
                options.UseRedis(configure: redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint(host: "127.0.0.1", port: 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                }, name: ConfigurationValues.DataBaseName);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BettingHouse", Version = "v1" });
            });

            services.AddScoped<IRouletteGameRepository, RouletteGameRepository>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<IValidator<BettingModel>, ValidateModel>();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "BettingHouse");
            });
        }
    }
}
