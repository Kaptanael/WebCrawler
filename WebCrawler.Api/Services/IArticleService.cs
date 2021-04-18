using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler.Api.Services
{
    public interface IArticleService 
    {
        Task<List<ArticleForListDto>> GetAllArticleAsync(CancellationToken cancellationToken = default);

        Task<ArticleForListDto> GetArticleByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<bool> InsertArticleAsync(ArticleForCreateDto articleForCreateDto, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> InsertArticlesAsync(List<ArticleForCreateDto> articlesForCreateDto, CancellationToken cancellationToken = default(CancellationToken));

        bool UpdateArticle(ArticleForUpdateDto articleForUpdateDto);

        bool DeleteArticle(long id);

    }
}
