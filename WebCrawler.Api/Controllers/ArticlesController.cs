﻿using Microsoft.AspNetCore.Mvc;
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
            _articleService = articleService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetArticle()
        {
            try
            {
                var articlesToReturn = await _articleService.GetAllAsync();

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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(long id)
        {
            try
            {
                var articleToReturn = await _articleService.GetByIdAsync(id);

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
        
        
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(article);
            }

            try
            {
                var messageToReturn = await _articleService.AddAsync(article);                
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
          
    }
}
