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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace StudyCore.Core.Context
{
    public class Repository<TEntity, T> : BaseRepository<TEntity, T>, IRepository<TEntity, T> where TEntity : BaseModel<T> where T : struct
    {
        /// <summary>判断对象是否存在</summary>
        /// <param name="whereLambd">条件表达式</param>
        /// <returns></returns>
        public virtual bool Any(Expression<System.Func<TEntity, bool>> whereLambd)
        {
            return Entities.Any(whereLambd);
        }
        /// <summary>判断对象是否存在</summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return Entities.Any();
        }

        /// <summary>
        /// 分页查询
        /// </summary
        /// <param name="whereLambd"></param>
        /// <param name="orderLambd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual MPage<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereLambd, Expression<Func<TEntity, bool>> orderLambd, int pageIndex, int pageSize, bool sort = true)
        {
            var result = new MPage<TEntity>();
            var entites = GetQueryable(whereLambd, orderLambd, sort);
            result.TotalCount = entites.Count();
            result.RowEntities = entites.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return result;
        }

        public virtual int Delete(T Id)
        {
            var entity = GetById(Id);
            if (entity != null)
            {
                entity.UpdateDateTime = DateTime.Now;
                entity.UpdateUserId = 1;
                entity.DataFlag = (int)BaseModelDataFlagType.无效;
                return Update(entity);
            }
            return 0;
        }

        public virtual Task<int> DeleteAsync(T Id)
        {
            var entity = GetById(Id);
            if (entity == null)
                throw new Exception("null");
            entity.UpdateDateTime = DateTime.Now;
            entity.UpdateUserId = 1;
            entity.DataFlag = (int)BaseModelDataFlagType.无效;
            return UpdateAsync(entity);
        }

        public virtual int Delete(IEnumerable<T> IdList)
        {
            var entitys = GetQueryable(t => IdList.Contains(t.Id));
            if (entitys == null)
                throw new Exception("null");
            entitys.ForEachAsync(t =>
            {
                t.UpdateDateTime = DateTime.Now;
                t.UpdateUserId = 1;
                t.DataFlag = (int)BaseModelDataFlagType.无效;
            });

            return Update(entitys);
        }

        public virtual Task<int> DeleteAsync(IEnumerable<T> IdList)
        {
            var entitys = GetQueryable(t => IdList.Contains(t.Id));
            if (entitys == null)
                throw new Exception("null");
            entitys.ForEachAsync(t =>
            {
                t.UpdateDateTime = DateTime.Now;
                t.UpdateUserId = 1;
                t.DataFlag = (int)BaseModelDataFlagType.无效;
            });
            return UpdateAsync(entitys);
        }
         
    }
}
