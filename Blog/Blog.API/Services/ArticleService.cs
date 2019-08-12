using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.API.Services.IServices;
using Blog.Core.Entities;
using Blog.Core.Enums;
using Blog.Core.Infrastructure;

namespace Blog.API.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddArticle(Article article)
        {
            _unitOfWork.ArticleRepository.Insert(article);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteArticle(Article article)
        {
            article.Status = Status.Passive;
            return await EditArticle(article);
        }

        public async Task<bool> EditArticle(Article article)
        {
            _unitOfWork.ArticleRepository.Update(article);
            return await _unitOfWork.SaveAsync();
        }

        public Article GetArticle(int id)
        {
            return _unitOfWork.ArticleRepository.GetById(id);
        }

        public IQueryable<Article> GetArticles(Expression<Func<Article, bool>> predicate = null, string includes = null)
        {
            return _unitOfWork.ArticleRepository.Get(predicate, includes);
        }
    }
}
