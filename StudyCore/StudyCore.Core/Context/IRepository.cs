using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudyCore.Data;

namespace StudyCore.Core.Context
{
    /// <summary>实体相关的仓储接口</summary>
    public interface IRepository<TEntity, T> where TEntity : BaseModel<T> where T : struct
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


        int Delete(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

        int Delete(IEnumerable<TEntity> entities);

        Task<int> DeleteAsync(IEnumerable<TEntity> entities);

    }
}
