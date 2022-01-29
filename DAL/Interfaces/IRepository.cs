using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Main interface to work with database objects
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IRepository<TModel> where TModel : class
    {
        /// <summary>
        /// Returns all items
        /// </summary>
        /// <returns></returns>
        Task<List<TModel>> GetAll();
        /// <summary>
        /// Add item asynchronicity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddAsync(TModel model);
        /// <summary>
        /// Add item asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TModel> GetByAsync(Expression<Func<TModel, bool>> expression);
        /// <summary>
        /// Add all items asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TModel>> GetAllByAsync(Expression<Func<TModel, bool>> expression);
        /// <summary>
        /// Delete item asynchronicity by parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task DeleteByAsync(Expression<Func<TModel, bool>> expression);
        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Update(TModel model);

    }
}
