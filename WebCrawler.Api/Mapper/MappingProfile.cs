using AutoMapper;

namespace WebCrawler.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleForListDto>();
            CreateMap<ArticleForCreateDto, Article>();
            CreateMap<ArticleForUpdateDto, Article>();
        }
    }
}
