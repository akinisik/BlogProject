using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.API.Services.IServices
{
    public interface IArticleService
    {
        Article GetArticle(int id);
        IQueryable<Article> GetArticles(Expression<Func<Article, bool>> predicate = null, string includes = null);
        Task<bool> AddArticle(Article article);
        Task<bool> DeleteArticle(Article article);
        Task<bool> EditArticle(Article article);
    }
}
