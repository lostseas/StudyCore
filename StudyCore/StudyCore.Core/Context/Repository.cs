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
    public class Repository<TEntity, T> : BaseRepository<TEntity, T> where TEntity : BaseModel<T> where T : struct
    {

        /// <summary>
        /// 获取数据库字符串的KEY
        /// </summary>
        /// <returns></returns>
        public string GetDbConnectionKey()
        {
            var str = "";
            return str;
        }

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

        /// <summary>异步提交</summary>
        /// <param name="model">新增实体对象</param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity model)
        {
            var context = GetDbContext();
            var entity = context.Set<TEntity>().Add(model).Entity;
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>批量新增未提交</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">新增集合</param>
        /// <returns></returns>
        public virtual async void AddRangeAsync(IEnumerable<TEntity> list)
        {
            var context = GetDbContext();
            await context.Set<TEntity>().AddRangeAsync(list);
            await context.SaveChangesAsync();
        }

        /// <summary>单个对象指定列修改</summary>
        /// <param name="model">要修改的实体对象</param>
        public virtual async Task<int> UpdateAsync(TEntity model)
        {
            int result;
            try
            {
                var context = this.GetDbContext();
                context.Set<TEntity>().Attach(model);
                PropertyInfo[] properties = model.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo propertyInfo = properties[i];
                    bool flag = propertyInfo.GetValue(model) != null || propertyInfo.GetValue(model).ToString() != DateTime.MinValue.ToString();
                    if (flag)
                    {
                        context.Entry<TEntity>(model).Property(propertyInfo.Name).IsModified = true;
                    }
                }
                result = await context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {

                throw new DbUpdateException("Update", exception);
            }
            return result;
        }

        /// <summary>
        /// 单个对象指定列修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="fileds">更改字段列表</param>
        public virtual async Task<int> UpdateAsync(TEntity model, List<string> fileds)
        {
            int result;
            try
            {
                var context = this.GetDbContext();
                context.Set<TEntity>().Attach(model);
                Type typeFromHandle = typeof(TEntity);
                IEnumerable<PropertyInfo> propertyInfos = typeFromHandle.GetProperties();
                var list = propertyInfos.Select(t => t.Name).ToList();
                foreach (var current in fileds)
                {
                    bool flag = list.Contains(current);
                    if (flag)
                    {
                        context.Entry(model).Property(current).IsModified = true;
                    }
                }
                result = await context.SaveChangesAsync();
            }
            catch (DbUpdateException innerException)
            {
                throw new DbUpdateException("Update", innerException);
            }
            return result;

        }
        /// <summary>批量修改</summary>
        /// <param name="list"></param>
        public virtual async Task<int> UpdateListAsync(List<TEntity> list)
        {
            try
            {
                if (list.Any())
                {
                    var entity = list.FirstOrDefault();
                    var modifyFiled = new List<string>();
                    PropertyInfo[] properties = entity.GetType().GetProperties();
                    for (var i = 0; i < properties.Length; i++)
                    {
                        PropertyInfo propertyInfo = properties[i];
                        var flag = propertyInfo.GetValue(entity) != null || propertyInfo.GetValue(entity).ToString() != DateTime.MinValue.ToString();
                        if (flag)
                        {
                            modifyFiled.Add(propertyInfo.Name);
                        }
                    }

                    var context = this.GetDbContext();
                    foreach (var current in list)
                    {
                        context.Set<TEntity>().Attach(current);
                        foreach (string file in modifyFiled)
                        {
                            context.Entry(current).Property(file).IsModified = true;
                        }
                    }
                    return await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException innerException)
            {
                throw new DbUpdateException("UpdateList", innerException);
            }
            return 0;
        }

        /// <summary>批量修改立刻提</summary>
        /// <param name="list">实体对象集合</param>
        /// <param name="fileds">更改字段列表</param>
        public virtual async Task<int> UpdateListAsync(List<TEntity> list, List<string> fileds)
        {
            try
            {
                var context = this.GetDbContext();
                foreach (var current in list)
                {
                    context.Set<TEntity>().Attach(current);
                    foreach (var current2 in fileds)
                    {
                        context.Entry(current).Property(current2).IsModified = true;
                    }
                }
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateException innerException)
            {
                throw new DbUpdateException("UpdateList", innerException);
            }
        }

        /// <summary>根据主键查询</summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(long Id)
        {
            var context = this.GetDbContext();
            return await context.Set<TEntity>().FindAsync(new object[]
            {
                Id
            });
        }

        /// <summary>根据主键查询</summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public virtual TEntity GetById(long Id)
        {
            var context = this.GetDbContext();
            return context.Set<TEntity>().Find(new object[]
            {
                Id
            });
        }

        /// <summary>获取默认一条数据，没有则为NULL</summary>
        /// <param name="whereLambda">条件表达式</param>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            var context = this.GetDbContext();
            return whereLambda == null ? context.Set<TEntity>().FirstOrDefault() : context.Set<TEntity>().FirstOrDefault(whereLambda);
        }

        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            var context = this.GetDbContext();
            return whereLambda == null ? context.Set<TEntity>() : context.Set<TEntity>().Where(whereLambda);
        }

        ///// <summary>带条件查询获取数据</summary>
        ///// <param name="whereLambda">条件表达式</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="ordering">排序可为空</param>
        ///// <param name="EconnMode">读/写</param>
        ///// <returns></returns>
        //public virtual List<T> GetList<T>(Expression<Func<T, bool>> whereLambda = null, string ordering = null)
        //{

        //}

        ///// <summary>获取数量</summary>
        ///// <param name="whereLambd">条件表达式</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="EconnMode">读/写</param>
        ///// <returns></returns>
        //public virtual int GetCount<T>(Expression<System.Func<T, bool>> whereLambd = null, string dbName = null, EConnectionMode EconnMode = EConnectionMode.Read) where T : class, new();
        ///// <summary>判断对象是否存在</summary>
        ///// <param name="whereLambd">条件表达式</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="EconnMode">读/写</param>
        ///// <returns></returns>
        //public virtual bool Any<T>(Expression<System.Func<T, bool>> whereLambd, string dbName = null, EConnectionMode EconnMode = EConnectionMode.Read) where T : class, new();
        ///// <summary>提交保存</summary>
        ///// <returns></returns>
        //public virtual int SaveChanges(string dbName = null);
        ///// <summary>回滚</summary>
        //public virtual void RollBackChanges(string dbName = null);

        ///// <summary>执行SQL语句不分页</summary>
        ///// <param name="SQL">如“SELECT ID FROM T WHERE ID=1”</param>
        ///// <param name="parms">参数列表</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="EConnectionMode">读/写</param>
        //public virtual List<T> ExecSql2<T>(string SQL, List<SqlParameter> parms)
        //{
        //}

        ///// <summary>执行存储过程语句</summary>
        ///// <param name="sql">SQL语句或存储过程名</param>
        ///// <param name="parms">参数列表</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="EConnectionMode">读/写</param>
        ///// <returns></returns>
        //public virtual List<T> ExecProce<T>(string sql, List<SqlParameter> parms, string dbName = null, EConnectionMode EConnectionMode = EConnectionMode.Read) where T : class, new();
        ///// <summary>返回影响的行数</summary>
        ///// <param name="sql">执行SQL语句</param>
        ///// <param name="parms">参数列表</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <returns></returns>
        //public virtual int ExecSqlCount(string sql, List<SqlParameter> parms, string dbName = null);
        ///// <summary>Update返回影响的行数</summary>
        ///// <param name="sql">SQL语句或存储过程名</param>
        ///// <param name="parms">参数列表</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <returns></returns>
        //public virtual int ExecProceCount<T>(string sql, List<SqlParameter> parms, string dbName = null) where T : class, new();
        ///// <summary>
        ///// 直接获取特定一个或者多个字段的值
        ///// 多个字段需要声明Model
        ///// </summary>
        ///// <typeparam name="T">返回类型（string,int,model）</typeparam>
        ///// <typeparam name="TEntity">查询实体</typeparam>
        ///// <param name="whereLambda">查询条件</param>
        ///// <param name="updateExpression">返回的表达式</param>
        ///// <param name="dbName">数据库</param>
        ///// <param name="EConnectionMode">读/写</param>
        ///// <returns></returns>
        //public virtual T GetTResult<T, TEntity>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, T>> updateExpression,  );
        ///// <summary>执行SQL返回</summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="SQL">SQL语句</param>
        ///// <param name="parms">参数</param>
        ///// <param name="dbName">数据库默认DYB</param>
        ///// <param name="EConnectionMode">读/写</param>
        ///// <returns></returns>
        //public virtual T GetXResult<T>(string SQL, List<SqlParameter> parms, );
        ///// <summary>执行查询SQL统计</summary>
        ///// <param name="SQL">SQL语句</param>
        ///// <param name="parms">参数</param>
        ///// <param name="dbName">数据库默认DYB</param>
        ///// <param name="EConnectionMode">读/写</param>
        ///// <returns></returns>
        //public virtual int QuerySqlCount(string SQL, List<SqlParameter> parms ;
        ///// <summary>查询数据获得DataTable</summary>
        ///// <param name="SQL">查询语句</param>
        ///// <param name="parms">参数列表</param>
        ///// <param name="dbName">DB数据库</param>
        ///// <param name="EconnMode">读/写</param>
        ///// <returns></returns>
        //public virtual DataTable ExecSqlDataTable(string Sql, List<SqlParameter> parms = null );
        private static List<string> GetModifyFiled<T>(T entity) where T : class, new()
        {
            var list = new List<string>();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo propertyInfo = properties[i];
                bool flag = propertyInfo.GetValue(entity) != null || propertyInfo.GetValue(entity).ToString() != DateTime.MinValue.ToString();
                if (flag)
                {
                    list.Add(propertyInfo.Name);
                }
            }
            return list;
        }
    }
}
