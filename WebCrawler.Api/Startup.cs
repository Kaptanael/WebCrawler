using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebCrawler.Api.Data;
using WebCrawler.Api.Repository;
using WebCrawler.Api.Services;

namespace WebCrawler.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void AddReposirories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IArticleRepository), typeof(ArticleRepository));
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IArticleService), typeof(ArticleService));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebCrawlerDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("WebCrawlerDbContext")));

            AddReposirories(services);

            AddServices(services);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCrawler.Api", Version = "v1" });
            });

            services.AddDbContext<WebCrawlerDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WebCrawlerDbContext")));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCrawler.Api v1"));
            }            

            app.UseRouting();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
