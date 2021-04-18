using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Api.Models;

namespace WebCrawler.Api.Data
{
    public class WebCrawlerDbContext : DbContext
    {
        public WebCrawlerDbContext (DbContextOptions<WebCrawlerDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebCrawler.Api.Models.Article> Article { get; set; }
    }
}
