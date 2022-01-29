using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Main interface to work with database objects
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly PhotoAlbunDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db context</param>
        public Repository(PhotoAlbunDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all items
        /// </summary>
        /// <returns></returns>
        public async Task<List<TModel>> GetAll()
        {
            return await _context.Set<TModel>().ToListAsync();
        }

        /// <summary>
        /// Add item asynchronicity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(TModel model)
        {
            await _context.Set<TModel>().AddAsync(model);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add item asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TModel> GetByAsync(Expression<Func<TModel, bool>> expression)
        {
            return await _context.Set<TModel>().FirstOrDefaultAsync(expression);
        }

        /// <summary>
        /// Add all items asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<TModel>> GetAllByAsync(Expression<Func<TModel, bool>> expression)
        {
            return await _context.Set<TModel>().Where(expression).ToListAsync();
        }

        /// <summary>
        /// Delete item asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task DeleteByAsync(Expression<Func<TModel, bool>> expression)
        {
            var itemToDelete = await _context.Set<TModel>().FirstOrDefaultAsync(expression);
            if (itemToDelete != null)
            {
                _context.Set<TModel>().Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Update(TModel model)
        {
            _context.Set<TModel>().Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
