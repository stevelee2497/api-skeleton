using API.Filters;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence.Contexts;
using Repository;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

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
            services.AddSingleton(Configuration);
            services.AddMvc(MvcOptions).AddNewtonsoftJson(JsonOption).SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSwaggerGen(SwaggerConfigs);
            services.AddDbContext<EfDbContext>(DbContextOptions);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRoleService, RoleService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(SwaggerUIConfigs);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(EnpointConfigs);

            DbInitializer.Initialize(app.ApplicationServices.CreateScope().ServiceProvider);
        }

        private void JsonOption(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void MvcOptions(MvcOptions options)
        {
            options.Filters.Add<ExceptionFilter>();
            options.Filters.Add<ValidatorActionFilter>();
        }

        private void DbContextOptions(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        private void SwaggerConfigs(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server API", Version = "v1" });
        }

        private void SwaggerUIConfigs(SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        }

        private void EnpointConfigs(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllers();
        }
    }
}
