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
    public interface IRepository<TEntity, T> : IBaseRepository<TEntity, T> where TEntity : BaseModel<T> where T : struct
    {

        new int Delete(T Id);

        new Task<int> DeleteAsync(T Id);

        int Delete(IEnumerable<T> IdList);

        Task<int> DeleteAsync(IEnumerable<T> IdList);


        /// <summary>判断对象是否存在</summary>
        /// <param name="whereLambd">条件表达式</param>
        /// <returns></returns>
        bool Any(Expression<System.Func<TEntity, bool>> whereLambd);
        /// <summary>判断对象是否存在</summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// 分页查询
        /// </summary
        /// <param name="whereLambd"></param>
        /// <param name="orderLambd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        MPage<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereLambd, Expression<Func<TEntity, bool>> orderLambd, int pageIndex, int pageSize, bool sort = true);
    }
}
