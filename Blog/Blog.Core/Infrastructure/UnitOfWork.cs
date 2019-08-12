using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Data;
using Blog.Core.Entities;

namespace Blog.Core.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private BlogDbContext _context;

        private GenericRepository<User> _userRepository;
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<Category> _categoryRepository;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
            _userRepository = new GenericRepository<User>(context);
            _articleRepository = new GenericRepository<Article>(context);
            _categoryRepository = new GenericRepository<Category>(context);
        }

        public IGenericRepository<User> UserRepository
        {
            get { return _userRepository; }
        }

        public IGenericRepository<Article> ArticleRepository
        {
            get { return _articleRepository; }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get { return _categoryRepository; }
        }
        
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
