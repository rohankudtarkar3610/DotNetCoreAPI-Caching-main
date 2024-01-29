using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Demo7.Repository.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all records from entity 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList();

        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="predicateOrderby"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, string>> predicateOrderby, bool orderByType);

        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="predicateOrderby"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> predicateOrderby, bool orderByType);

        /// <summary>
        /// GetByCondition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetByCondition(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Insert(T entity);
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Update(T entity);

        IEnumerable<T> ExecuteProcedure(string query, params object[] parameters);
    }
}
