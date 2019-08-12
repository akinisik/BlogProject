using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.API.Services.IServices
{
    public interface ICategoryService
    {
        Category GetCategory(int id);
        IQueryable<Category> GetCategories(Expression<Func<Category, bool>> predicate = null, string includes = null);
        Task<bool> AddCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<bool> EditCategory(Category category);
    }
}
