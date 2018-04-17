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
        /// 获取数据库字符串的KEY
        /// </summary>
        /// <returns></returns>
        string GetDbConnectionKey();

        /// <summary>
        /// 获得Dbcontext
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();

        /// <summary>异步提交</summary>
        /// <param name="model">新增实体对象</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity model);

        /// <summary>批量新增未提交</summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">新增集合</param>
        /// <returns></returns>
        void AddRangeAsync(IEnumerable<TEntity> list);

        /// <summary>单个对象指定列修改</summary>
        /// <param name="model">要修改的实体对象</param>
        Task<int> UpdateAsync(TEntity model);

        /// <summary>
        /// 单个对象指定列修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="fileds">更改字段列表</param>
        Task<int> UpdateAsync(TEntity model, List<string> fileds);

        /// <summary>批量修改</summary>
        /// <param name="list"></param>
        Task<int> UpdateListAsync(List<TEntity> list);

        /// <summary>批量修改立刻提</summary>
        /// <param name="list">实体对象集合</param>
        /// <param name="fileds">更改字段列表</param>
        Task<int> UpdateListAsync(List<TEntity> list, List<string> fileds);

        /// <summary>根据主键查询</summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(T Id);

        /// <summary>根据主键查询</summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        TEntity GetById(T Id);

        /// <summary>获取默认一条数据，没有则为NULL</summary>
        /// <param name="whereLambda">条件表达式</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null);

        /// <summary>带条件查询获取数据</summary>
        /// <param name="whereLambda">条件表达式</param>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereLambda = null);

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
    }
}
