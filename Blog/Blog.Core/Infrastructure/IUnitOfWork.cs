using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Article> ArticleRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        bool Save();
        Task<bool> SaveAsync();
    }
}
