
using DataAccess.Data;
using Demo7.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using Demo7.DataAccess;

namespace Demo7.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>
      where T : class
    {
        internal PanditClinicContext _entities;
        internal DbSet<T> _dbset;
        public IQueryable<T> _dbsetQueryble { get; }

        public GenericRepository(PanditClinicContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
            _dbsetQueryble = context.Set<T>().AsNoTracking();
        }

        public GenericRepository()
        {

        }
        /// <summary>
        /// Get all records from entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetList()
        {
            return await Task.FromResult(_dbsetQueryble.AsNoTracking().AsEnumerable<T>());
        }

        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicateWhere"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> predicateWhere)
        {
            return await Task.FromResult(_dbsetQueryble.Where(predicateWhere).AsNoTracking().AsEnumerable<T>());
        }

        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicateOrderby"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListByCondition(System.Linq.Expressions.Expression<Func<T, string>> predicateOrderby, bool orderByType)
        {
            if (orderByType == true)
                return await Task.FromResult(_dbsetQueryble.OrderBy(predicateOrderby).AsNoTracking().AsEnumerable<T>());
            else
                return await Task.FromResult(_dbsetQueryble.OrderByDescending(predicateOrderby).AsNoTracking().AsEnumerable<T>());
        }


        /// <summary>
        /// Get List By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="predicateOrderby"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListByCondition(Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, string>> predicateOrderby, bool orderByType)
        {
            if (orderByType == true)
                return await Task.FromResult(_dbsetQueryble.Where(predicate).OrderBy(predicateOrderby).AsNoTracking().AsEnumerable<T>());
            else
                return await Task.FromResult(_dbsetQueryble.Where(predicate).OrderByDescending(predicateOrderby).AsNoTracking().AsEnumerable<T>());
        }

        /// <summary>
        /// GetByCondition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T?> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbsetQueryble.Where(predicate).AsNoTracking().FirstOrDefault<T>());
        }

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbset.Where(predicate).AsNoTracking().AsEnumerable<T>());
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Insert(T entity)
        {
            try
            {
                _entities.Add(entity);
                _entities.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Update(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
                _entities.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> EagerLoadEntity(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbsetQueryble;
            foreach (var includeProperty in navigationProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> EagerLoadEntity(Expression<Func<T, bool>> wherePredicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbsetQueryble;
            foreach (var includeProperty in navigationProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(wherePredicate).AsNoTracking().ToListAsync();
        }
        void Save(bool applyState = true, T entity = null, List<string> propertiesToIgnore = null)
        {
            try
            {
                if (applyState)
                    _entities.ApplyStateChanges();
                if (propertiesToIgnore != null)
                {
                    foreach (var prop in propertiesToIgnore)
                    {
                        _entities.Entry<T>(entity).Property(prop).IsModified = false;
                    }

                }
                _entities.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<T>> ExecuteSQL(string command)
        {
            var data = await Task.FromResult(_entities.Set<T>().FromSqlRaw(command).AsNoTracking());
            return data;
        }
        private IEnumerable<Dictionary<string, object>> ConvertToDictionary(System.Data.Common.DbDataReader reader)
        {
            List<Dictionary<string, object>> rows = null;
            rows = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var type = reader.GetDataTypeName(i);
                    var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);

                    //if (columnValue == null)
                    //{
                    //    columnValue = "";
                    //}

                    row.Add(columnName, columnValue);
                }
                rows.Add(row);
            }

            return rows;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_entities != null)
                {
                    _entities.Dispose();
                    _entities = null;
                }
            }
        }

        public IEnumerable<T> ExecuteProcedure(string query, params object[] parameters)
        {
            if (parameters.Count() == 0)
                return _dbset.FromSqlRaw<T>(query).ToList();
            else
                return _dbset.FromSqlRaw<T>(query, parameters).ToList();
        }
    }
}
