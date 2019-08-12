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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddUser(User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteUser(User user)
        {
            user.Status = Status.Passive;
            return await EditUser(user);
        }

        public async Task<bool> EditUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            return await _unitOfWork.SaveAsync();
        }

        public User GetUser(int id)
        {
            return _unitOfWork.UserRepository.GetById(id);
        }

        public IQueryable<User> GetUsers(Expression<Func<User, bool>> predicate = null, string includes = null)
        {
            return _unitOfWork.UserRepository.Get(predicate, includes);
        }
    }
}
