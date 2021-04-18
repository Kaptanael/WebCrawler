using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebCrawler.Api.Data;
using WebCrawler.Api.Services;

namespace WebCrawler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleService _articleService;

        public ArticlesController(ILogger<ArticlesController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        [Route("get-article")]
        [HttpGet]
        public async Task<IActionResult> GetArticle()
        {
            try
            {
                var articlesToReturn = await _articleService.GetAllArticleAsync();

                if (articlesToReturn == null)
                {
                    return NotFound();
                }

                return Ok(articlesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("get-article/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArticle(long id)
        {
            try
            {
                var articleToReturn = await _articleService.GetArticleByIdAsync(id);

                if (articleToReturn == null)
                {
                    return NotFound();
                }

                return Ok(articleToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("save-article")]
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] ArticleForCreateDto article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(article);
            }

            try
            {
                var result = await _articleService.InsertArticleAsync(article);
                return StatusCode((int)HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("save-articles")]
        [HttpPost]
        public async Task<IActionResult> PostArticles([FromBody] List<ArticleForCreateDto> articles)
        {
            try
            {
                var result = await _articleService.InsertArticlesAsync(articles);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
