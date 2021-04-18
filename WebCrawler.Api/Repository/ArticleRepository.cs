using WebCrawler.Api.Data;

namespace WebCrawler.Api.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(WebCrawlerDbContext context) : base(context)
        {
        }
    }
}
