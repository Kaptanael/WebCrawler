using WebCrawler.Api.Repository;

namespace WebCrawler.Api.Services
{
    public class ArticleService: BaseService<Article>, IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository) : base(articleRepository)
        {
            _articleRepository = articleRepository;
        }        
    }
}
