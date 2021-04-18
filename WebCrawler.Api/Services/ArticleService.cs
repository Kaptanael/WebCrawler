using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebCrawler.Api.Repository;

namespace WebCrawler.Api.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        public ArticleService(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<List<ArticleForListDto>> GetAllArticleAsync(CancellationToken cancellationToken = default)
        {
            var articlesFromRepo = await _articleRepository.GetAllAsync(null, null, null, cancellationToken);
            var articlesToReturn = _mapper.Map<List<ArticleForListDto>>(articlesFromRepo);

            return articlesToReturn;
        }

        public async Task<ArticleForListDto> GetArticleByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var articleFromRepo = await _articleRepository.GetByIdAsync(id, cancellationToken);
            var articleToReturn = _mapper.Map<ArticleForListDto>(articleFromRepo);

            return articleToReturn;
        }

        public async Task<bool> InsertArticleAsync(ArticleForCreateDto articleForCreateDto, CancellationToken cancellationToken = default(CancellationToken))
        {
            var articleToCreate = new Article
            {
                Date = articleForCreateDto.Date,
                Title = articleForCreateDto.Title,
                Url = articleForCreateDto.Url,
                Archived = false
            };

            await _articleRepository.AddAsync(articleToCreate, cancellationToken);
            var result = await _articleRepository.SaveAsync(cancellationToken);

            return result;
        }

        public async Task<bool> InsertArticlesAsync(List<ArticleForCreateDto> articlesForCreateDto, CancellationToken cancellationToken = default)
        {
            var articlesToCreate = _mapper.Map<IEnumerable<Article>>(articlesForCreateDto);
            await _articleRepository.AddRangeAsync(articlesToCreate, cancellationToken);
            var result = await _articleRepository.SaveAsync(cancellationToken);

            return result;
        }

        public bool UpdateArticle(ArticleForUpdateDto articleForUpdateDto)
        {
            var articleToUpdate = new Article
            {
                Id = articleForUpdateDto.Id,
                Date = articleForUpdateDto.Date,
                Title = articleForUpdateDto.Title,
                Url = articleForUpdateDto.Url,
                Archived = articleForUpdateDto.Archived
            };

            _articleRepository.Update(articleToUpdate);
            var result = _articleRepository.Save();

            return result;
        }

        public bool DeleteArticle(long id)
        {
            _articleRepository.Delete(id);
            var result = _articleRepository.Save();
            return result;
        }        
    }
}
