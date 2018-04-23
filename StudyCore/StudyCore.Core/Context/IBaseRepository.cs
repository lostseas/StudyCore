using StudyCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudyCore.Core.Context
{
    public interface IBaseRepository<TEntity, T>
        where TEntity : BaseModel<T>
        where T : struct
    {

        /// <summary>
        /// 获得Dbcontext
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// get entity by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntity GetById(object Id);

        /// <summary>
        /// get entity by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object Id);
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <param name="entity">新增实体对象</param>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>批量新增</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entities">新增集合</param>
        /// <returns></returns>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>批量新增</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entities">新增集合</param>
        /// <returns></returns>
        void AddAsync(IEnumerable<TEntity> entities);

        /// <summary>单个对象指定列修改</summary>
        /// <param name="entity">要修改的实体对象</param>
        int Update(TEntity entity);

        /// <summary>单个对象指定列修改</summary>
        /// <param name="entity">要修改的实体对象</param>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>批量修改</summary>
        /// <param name="entities"></param>
        int Update(IEnumerable<TEntity> entities);


        /// <summary>批量修改</summary>
        /// <param name="entities"></param>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);


        int Delete(T Id);

        Task<int> DeleteAsync(T Id);

        int Delete(IEnumerable<T> IdList);

        Task<int> DeleteAsync(IEnumerable<T> IdList);


        /// <summary>获取默认一条数据，没有则为NULL</summary>
        /// <param name="whereLambda">条件表达式</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda);

        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序可为空</param>
        /// <param name="sort">排序方式</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, bool>> orderLambda, bool sort = true);
    }