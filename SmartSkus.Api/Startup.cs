using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using SmartSkus.Api.Data;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Data.Repo;
using System;

namespace SmartSkus.Api
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
            // SQL Local Db used for Development
            //services.AddDbContext<InventoryContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // EF Core InMemory Database
            services.AddDbContext<InventoryContext>(options =>
                options.UseInMemoryDatabase(databaseName: "InventoryDb"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Samrt Sku API",
                    Version = "v1",
                    Description = "Smart Sku, Master Data and Inventory Mangement APIs",
                    Contact = new OpenApiContact 
                    { 
                        Name = "Avani",
                        Email = "avanisoam@gmail.com",
                        Url = new Uri("https://github.com/avanisoam"),
                    }
                });
            });

            services.AddScoped<IInventoryRepo, InventoryRepo>();
            services.AddScoped<IMasterDataRepo, MasterDataRepo>();
            services.AddScoped<ISkuRepo, SkuRepo>();
            services.AddScoped<ISettingRepo, SettingRepo>();

            services.AddControllers().AddNewtonsoftJson(
                x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy =>
             policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
