using JD.Utils.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JD.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        #region 查找数据
        long Count(Expression<Func<TEntity, bool>> predicate = null);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<TEntity> GetAsync(TKey id);
        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, string ordering, bool isNoTracking);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering, bool isNoTracking);

        #endregion

        #region 插入数据
        bool Insert(TEntity entity, bool isSaveChange);
        Task<bool> InsertAsync(TEntity entity, bool isSaveChange);
        bool Insert(List<TEntity> entitys, bool isSaveChange = true);
        Task<bool> InsertAsync(List<TEntity> entitys, bool isSaveChange);
        #endregion

        #region 删除(删除之前需要查询)
        bool Delete(TEntity entity, bool isSaveChange);
        bool Delete(List<TEntity> entitys, bool isSaveChange);
        Task<bool> DeleteAsync(TEntity entity, bool isSaveChange);
        Task<bool> DeleteAsync(List<TEntity> entitys, bool isSaveChange = true);
        #endregion

        #region 修改数据
        bool Update(TEntity entity, bool isSaveChange, List<string> updatePropertyList);
        Task<bool> UpdateAsync(TEntity entity, bool isSaveChange, List<string> updatePropertyList);
        bool Update(List<TEntity> entitys, bool isSaveChange);
        Task<bool> UpdateAsync(List<TEntity> entitys, bool isSaveChange);
        #endregion

        #region 执行Sql语句
        void BulkInsert<T>(List<T> entities);
        int ExecuteSql(string sql);
        Task<int> ExecuteSqlAsync(string sql);
        int ExecuteSql(string sql, List<DbParameter> spList);
        Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList);
        DataTable GetDataTableWithSql(string sql);
        DataTable GetDataTableWithSql(string sql, List<DbParameter> spList);

        #endregion


        #region 分页查找
        /// <summary>
        /// 分页查询异步
        /// </summary>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <param name="orderBy">排序条件（一定要有）</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="isOrder">排序正反</param>
        /// <returns></returns>
        Task<PageData<TEntity>> GetPageAsync<TResult>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TResult>> orderBy, int pageIndex, int pageSize, bool isOrder = true, bool isNoTracking = true);
        /// <summary>
        /// 分页查询异步
        /// </summary>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <param name="ordering">排序条件（一定要有，多个用逗号隔开，倒序开头用-号）</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        Task<PageData<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> whereLambda, string ordering, int pageIndex, int pageSize, bool isNoTracking = true);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <param name="ordering">排序条件（一定要有，多个用逗号隔开，倒序开头用-号）</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        PageData<TEntity> GetPage(Expression<Func<TEntity, bool>> whereLambda, string ordering, int pageIndex, int pageSize, bool isNoTracking = true);
        #endregion
    }
}
