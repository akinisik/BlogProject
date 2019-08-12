using Blog.API.Services.IServices;
using Blog.Core.Entities;
using Blog.Core.Enums;
using Blog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Insert(category);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            category.Status = Status.Passive;
            return await EditCategory(category);
        }

        public async Task<bool> EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            return await _unitOfWork.SaveAsync();
        }

        public IQueryable<Category> GetCategories(Expression<Func<Category, bool>> predicate = null, string includes = null)
        {
            return _unitOfWork.CategoryRepository.Get(predicate, includes);
        }

        public Category GetCategory(int id)
        {
            return _unitOfWork.CategoryRepository.GetById(id);
        }
    }
}
