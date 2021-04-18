using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Api.Entity;

namespace WebCrawler.Api.EntityConfiguration
{
    public class ArticleConfig: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);            
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Url).HasMaxLength(1024).IsRequired();
            builder.Property(a => a.CrawledDate).HasDefaultValueSql("GETDATE()");

            builder.ToTable("Article");
        }
    }
}
