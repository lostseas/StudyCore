using Microsoft.EntityFrameworkCore;
using StudyCore.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace StudyCore.Core.Context
{
    public abstract partial class BaseRepository<TEntity, T> : IBaseRepository<TEntity, T> where TEntity : BaseModel<T> where T : struct
    {
        #region cort
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

        public BaseRepository()
        {
            _context = GetDbContext();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获得Dbcontext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            var sqlConnectString = GetDbConnectionKey();
            var options = new DbContextOptionsBuilder().UseSqlServer(sqlConnectString).Options;
            DbContext context = new StudyCoreDbContext(options);
            return context;
        }

        /// <summary>
        /// get entity by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(object Id)
        {
            return Entities.Find(Id);
        }

        /// <summary>
        /// get entity by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(object Id)
        {
            return Entities.FindAsync(Id);
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                var model = Entities.Add(entity).Entity;
                _context.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <param name="entity">新增实体对象</param>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                var model = Entities.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>批量新增</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entities">新增集合</param>
        /// <returns></returns>
        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            _context.SaveChanges();
        }

        /// <summary>批量新增</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entities">新增集合</param>
        /// <returns></returns>
        public virtual async void AddAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }


        /// <summary>单个对象指定列修改</summary>
        /// <param name="entity">要修改的实体对象</param>
        public virtual int Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new Exception("Update entity is null");

                return _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {

                throw new DbUpdateException("Update", exception);
            }
        }

        /// <summary>单个对象指定列修改</summary>
        /// <param name="entity">要修改的实体对象</param>
        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new Exception("Update entity is null");

                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }

        /// <summary>批量修改</summary>
        /// <param name="entities"></param>
        public virtual int Update(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new Exception("Update entitys is null");

                return _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }

        /// <summary>批量修改</summary>
        /// <param name="entities"></param>
        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new Exception("Update entitys is null");

                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }


        public virtual int Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new Exception("Update entity is null");

                Entities.Remove(entity);
                return _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new Exception("Update entity is null");

                Entities.Remove(entity);
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }
        public virtual int Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new Exception("Update entities is null");

                Entities.RemoveRange(entities);
                return _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }
        public virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new Exception("Update entities is null");

                Entities.RemoveRange(entities);
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Update", exception);
            }
        }


        #endregion


        /// <summary>获取默认一条数据，没有则为NULL</summary>
        /// <param name="whereLambda">条件表达式</param>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda)
        {
            return Entities.FirstOrDefault(whereLambda);
        }

        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda)
        {
            return Entities.Where(whereLambda);
        }

        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序可为空</param>
        /// <param name="sort">排序方式</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, bool>> orderLambda, bool sort = true)
        {
            if (sort)
            {
                return Entities.Where(whereLambda).OrderBy(orderLambda);
            }
            else
            {
                return Entities.Where(whereLambda).OrderByDescending(orderLambda);
            }

        }




        #region Properties

        /// <summary>
        /// 获取数据库字符串的KEY
        /// </summary>
        /// <returns></returns>
        protected string GetDbConnectionKey()
        {
            var str = "";
            return str;
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();
                return _entities;
            }
        }

        #endregion
    }
}
