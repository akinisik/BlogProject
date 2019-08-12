using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.API.Services.IServices
{
    public interface IUserService
    {
        User GetUser(int id);
        IQueryable<User> GetUsers(Expression<Func<User, bool>> predicate = null, string includes = null);
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> EditUser(User user);
    }
}
