using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace DMS.EFCore.Extension
{
    /// <summary>
    /// 服务基类实现
    /// </summary>
    public class ServiceBasebak : IServiceBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext DbContext { get { return _context; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ServiceBasebak(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取查询构造器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }

        #region Get操作
        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return _context.Set<T>().FirstOrDefault(predicate);
            }
            return _context.Set<T>().FirstOrDefault();
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            return await _context.Set<T>().FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取第一条数据
        /// 如果查不数据直接报错，建议try起来
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T First<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                try
                {
                    return _context.Set<T>().First(predicate);
                }
                catch
                {
                    return default(T);
                }
            }
            return _context.Set<T>().First();
        }
        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                try
                {
                    return await _context.Set<T>().FirstAsync(predicate);
                }
                catch
                {
                    return default(T);
                }
            }
            return await _context.Set<T>().FirstAsync();
        }
        /// <summary>
        /// 获取第最后一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T LastOrDefault<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return _context.Set<T>().LastOrDefault(predicate);
            }
            return _context.Set<T>().LastOrDefault();
        }
        public async Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return await _context.Set<T>().LastOrDefaultAsync(predicate);
            }
            return await _context.Set<T>().LastOrDefaultAsync();
        }
        /// <summary>
        /// 查询多条集合数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate == null)
            {
                return _context.Set<T>().AsNoTracking().ToList();
            }
            else
            {
                return _context.Set<T>().Where(predicate).ToList();
            }
        }
        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate == null)
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
        }
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyVaules"></param>
        /// <returns></returns>
        public T GetByKey<T>(params object[] keyVaules) where T : class
        {
            return _context.Set<T>().Find(keyVaules);
        }
        public async Task<T> GetByKeyAsync<T>(params object[] keyVaules) where T : class
        {
            return await _context.Set<T>().FindAsync(keyVaules);
        }
        #endregion

        #region 新增实体
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return _context.Insert(entity);
            }
            return 0;
        }
        public async Task<long> InsertAsync<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return await _context.InsertAsync(entity);
            }
            return 0;
        }
        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return _context.Insert<T>(entities);
            }
            return 0;
        }
        public async Task<long> InsertAsync<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return await _context.InsertAsync<T>(entities);
            }
            return 0;
        }
        #endregion

        #region 删除实体
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return _context.Delete(entity);
            }
            return 0;
        }
        public async Task<long> DeleteAsync<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return await _context.DeleteAsync(entity);
            }
            return 0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Delete<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return _context.Delete(entities);
            }
            return 0;
        }
        public async Task<long> DeleteAsync<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return await _context.DeleteAsync(entities);
            }
            return 0;
        }
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Delete(predicate);
        }
        public async Task<long> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.DeleteAsync(predicate);
        }
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete<T>(int key) where T : class
        {
            T entity = GetByKey<T>(key);
            if (entity != null)
            {
                return Delete(entity);
            }
            return 0;
        }
        public async Task<long> DeleteAsync<T>(int key) where T : class
        {
            T entity = await GetByKeyAsync<T>(key);
            if (entity != null)
            {
                return await DeleteAsync(entity);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete<T>(Guid key) where T : class
        {
            T entity = this.GetByKey<T>(key);
            if (entity != null)
            {
                return Delete(entity);
            }
            return 0;
        }
        public async Task<long> DeleteAsync<T>(Guid key) where T : class
        {
            T entity = await GetByKeyAsync<T>(key);
            if (entity != null)
            {
                return await DeleteAsync(entity);
            }
            return 0;
        }

        #endregion

        #region 修改实体
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return _context.Modifiy(entity);
            }
            return 0;
        }
        public async Task<long> UpdateAsync<T>(T entity) where T : class
        {
            if (entity != null)
            {
                return await _context.ModifiyAsync(entity);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Update<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return _context.Modifiy<T>(entities);
            }
            return 0;
        }
        public async Task<long> UpdateAsync<T>(List<T> entities) where T : class
        {
            if (entities != null && entities.Count > 0)
            {
                return await _context.ModifiyAsync<T>(entities);
            }
            return 0;
        }
        /// <summary>
        /// 目前还不支持
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int Update<T>(Expression<Func<T, T>> set, Expression<Func<T, bool>> where) where T : class
        {
            return _context.Modifiy(set, where);
        }
        public async Task<long> UpdateAsync<T>(Expression<Func<T, bool>> set, Expression<Func<T, bool>> where) where T : class
        {
            return await _context.ModifiyAsync(set, where);
        }
        #endregion

        #region Count
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return _context.Set<T>().Count(predicate);
            }
            return _context.Set<T>().Count();
        }
        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return await _context.Set<T>().CountAsync(predicate);
            }
            return await _context.Set<T>().CountAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public long LongCount<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return _context.Set<T>()
                               .LongCount(predicate);
            }
            return _context.Set<T>().LongCount();
        }
        public async Task<long> LongCountAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
            {
                return await _context.Set<T>().LongCountAsync(predicate);
            }
            return await _context.Set<T>().LongCountAsync();
        }
        #endregion

        #region 批量更新，删除
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int BatchUpdate<T>(Expression<Func<T, T>> set, Expression<Func<T, bool>> where) where T : class, new()
        {
            return _context.Set<T>().Where(where).BatchUpdate(set);
        }
        public async Task<int> BatchUpdateAsync<T>(Expression<Func<T, T>> set, Expression<Func<T, bool>> where) where T : class, new()
        {
            return await _context.Set<T>().Where(where).BatchUpdateAsync(set);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int DeleteBulk<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return _context.Set<T>().Where(where).BatchDelete();
        }
        public async Task<int> DeleteBulkAsync<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return await _context.Set<T>().Where(where).BatchDeleteAsync();
        }
        #endregion


        /// <summary>
        /// 析构函数
        /// </summary>
        ~ServiceBasebak()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}