using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Blog.API.Models;
using Blog.API.Services.IServices;
using Blog.Core.Entities;
using Blog.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [Authorize]
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetCategory(int id, string includes = null)
        {
            try
            {
                var category = _categoryService.GetCategories(x => x.Id == id && x.Status == Status.Active, includes).FirstOrDefault();
                if (category != null)
                {
                    return Ok(ModelFactory.CreateModel(category));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        

        [HttpGet]
        public IActionResult GetCategories(string includes = null)
        {
            try
            {
                var categories = _categoryService.GetCategories(x => x.Status == Status.Active, includes);
                IEnumerable<CategoryModel> results = categories.Select(x => ModelFactory.CreateModel(x));
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateModel createCategoryModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var category = Mapper.Map<CategoryCreateModel, Category>(createCategoryModel);
                category.Status = Status.Active;
                await _categoryService.AddCategory(category);
                return Ok(ModelFactory.CreateModel(category));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = _categoryService.GetCategories(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
                if (category != null)
                {
                    category.Status = Status.Passive;
                    await _categoryService.DeleteCategory(category);
                    return Ok();
                }
                return BadRequest(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}