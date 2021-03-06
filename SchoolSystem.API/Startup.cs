using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentMigrator.Runner;
using System.Reflection;
using System.Data;
using Newtonsoft.Json.Serialization;
using SchoolSystem.Services.Repositories.Interfaces;
using SchoolSystem.Services.Repositories.Repositories;
using Npgsql;
using SchoolSystem.Services.Services.Interfaces;
using SchoolSystem.Services.Services.Services;


namespace SchoolSystem.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //JSON Serializer
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                        .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver());
            
            
            string dbConnectionString = _config.GetConnectionString("DefaultConnection");

            services.AddTransient<IDbConnection>(connection => new NpgsqlConnection(dbConnectionString));
            
            services.AddScoped<ISchoolServices, SchoolServices>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();



            services.AddFluentMigratorCore().ConfigureRunner(c => c
                    .AddPostgres()
                    .WithGlobalConnectionString("DefaultConnection")
                    .ScanIn(Assembly.Load("SchoolSystem.Migrations")).For.All())
                .AddLogging(c => c.AddFluentMigratorConsole());
            
            
            services.AddControllers();

            
          

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SchoolSystem.API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolSystem.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            using var scope = app.ApplicationServices.CreateScope();
            var migration = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            migration.MigrateUp();
        }
    }
}