using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.API.Models;
using Blog.API.Services.IServices;
using Blog.Core.Entities;
using Blog.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// Get Artice by id.
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetArticle(int id, string includes = null)
        {
            try
            {
                var article = _articleService.GetArticles(x => x.Id == id && x.Status == Status.Active, includes).FirstOrDefault();
                if (article != null)
                {
                    return Ok(ModelFactory.CreateModel(article));
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Artice list.
        /// </summary>
        [HttpGet]
        public IActionResult GetArticles(string includes = null)
        {
            try
            {
                var articles = _articleService.GetArticles(x => x.Status == Status.Active, includes);
                IEnumerable<ArticleModel> results = articles.Select(x => ModelFactory.CreateModel(x));
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        ///     POST /api/article
        ///     {
        ///        "categoryId": 1,
        ///        "title": "Article Title",
        ///        "content": "Article Content"
        ///     }
        ///
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] ArticleCreateModel articleCreateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var article = Mapper.Map<ArticleCreateModel, Article>(articleCreateModel);
                article.UserId = 1;
                article.Status = Status.Active;
                await _articleService.AddArticle(article);
                return Ok(ModelFactory.CreateModel(article));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var article = _articleService.GetArticles(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
                if (article != null)
                {
                    await _articleService.DeleteArticle(article);
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}