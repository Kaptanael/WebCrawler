using Microsoft.EntityFrameworkCore;
using WebCrawler.Api.Entity;
using WebCrawler.Api.EntityConfiguration;

namespace WebCrawler.Api.Data
{
    public class WebCrawlerDbContext : DbContext
    {
        public WebCrawlerDbContext (DbContextOptions<WebCrawlerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Article>(new ArticleConfig());            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Article> Article { get; set; }
    }
}
