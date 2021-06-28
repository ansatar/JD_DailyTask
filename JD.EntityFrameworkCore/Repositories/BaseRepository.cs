using JD.EntityFrameworkCore.EntityFrameworkCore;
using JD.Utils;
using JD.Utils.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public JDDailyTaskDbContext _dbContext { get; } = null;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string _connectionString { get; set; }

        public BaseRepository(JDDailyTaskDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public DatabaseFacade Database => _dbContext.Database;
        public IQueryable<TEntity> Entities => _dbSet.AsQueryable().AsNoTracking();
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public bool Any(Expression<Func<TEntity, bool>> whereLambd)
        {
            return _dbSet.Where(whereLambd).Any();
        }
        #region 插入数据
        public bool Insert(TEntity entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public async Task<bool> InsertAsync(TEntity entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        public bool Insert(List<TEntity> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public async Task<bool> InsertAsync(List<TEntity> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion

        #region 删除
        public bool Delete(TEntity entity, bool isSaveChange = true)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return isSaveChange ? SaveChanges() > 0 : false;
        }
        public bool Delete(List<TEntity> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange ? SaveChanges() > 0 : false;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity, bool isSaveChange = true)
        {

            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return isSaveChange ? await SaveChangesAsync() > 0 : false;
        }
        public virtual async Task<bool> DeleteAsync(List<TEntity> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange ? await SaveChangesAsync() > 0 : false;
        }
        #endregion

        #region 更新数据
        public bool Update(TEntity entity, bool isSaveChange = true, List<string> updatePropertyList = null)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {

                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });


            }
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public bool Update(List<TEntity> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                Update(c, false);
            });
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(TEntity entity, bool isSaveChange = true, List<string> updatePropertyList = null)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry<TEntity>(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {
                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });

            }
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(List<TEntity> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                _dbSet.Attach(c);
                _dbContext.Entry<TEntity>(c).State = EntityState.Modified;
            });
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion

        #region 查找
        public long Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return _dbSet.LongCount(predicate);
        }
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await _dbSet.LongCountAsync(predicate);
        }
        public TEntity Get(TKey id)
        {
            if (id == null)
            {
                return default(TEntity);
            }
            return _dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            return data.FirstOrDefault();
        }


        public async Task<TEntity> GetAsync(TKey id)
        {
            if (id == null)
            {
                return default(TEntity);
            }
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            return await data.FirstOrDefaultAsync();
        }


        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            if (!string.IsNullOrEmpty(ordering))
            {
                data = data.OrderByBatch(ordering);
            }
            return await data.ToListAsync();
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            if (!string.IsNullOrEmpty(ordering))
            {
                data = data.OrderByBatch(ordering);
            }
            return data.ToList();
        }
        public async Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await Task.Run(() => isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate));
        }
        public IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
        }
        #endregion

        #region SQL语句
        public virtual void BulkInsert<T>(List<T> entities)
        { }
        public int ExecuteSql(string sql)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql);
        }

        public Task<int> ExecuteSqlAsync(string sql)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql);
        }

        public int ExecuteSql(string sql, List<DbParameter> spList)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, spList.ToArray());
        }

        public Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql, spList.ToArray());
        }


        public virtual DataTable GetDataTableWithSql(string sql)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetDataTableWithSql(string sql, List<DbParameter> spList)
        {
            throw new NotImplementedException();
        }

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
        public async Task<PageData<TEntity>> GetPageAsync<TResult>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TResult>> orderBy, int pageIndex, int pageSize, bool isOrder = true, bool isNoTracking = true)
        {
            IQueryable<TEntity> data = isOrder ?
                _dbSet.OrderBy(orderBy) :
                _dbSet.OrderByDescending(orderBy);

            if (whereLambda != null)
            {
                data = isNoTracking ? data.Where(whereLambda).AsNoTracking() : data.Where(whereLambda);
            }
            PageData<TEntity> pageData = new PageData<TEntity>
            {
                Totals = await data.CountAsync(),
                Rows = await data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
            return pageData;
        }

        /// <summary>
        /// 分页查询异步
        /// </summary>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <param name="ordering">排序条件（一定要有，多个用逗号隔开，倒序开头用-号）</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public async Task<PageData<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> whereLambda, string ordering, int pageIndex, int pageSize, bool isNoTracking = true)
        {
            // 分页 一定注意： Skip 之前一定要 OrderBy
            if (string.IsNullOrEmpty(ordering))
            {
                ordering = nameof(TEntity) + "Id";//默认以Id排序
            }
            var data = _dbSet.OrderByBatch(ordering);
            if (whereLambda != null)
            {
                data = isNoTracking ? data.Where(whereLambda).AsNoTracking() : data.Where(whereLambda);
            }
            //查看生成的sql，找到大数据下分页巨慢原因为order by 耗时
            //var sql = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToSql();
            //File.WriteAllText(@"D:\sql.txt",sql);
            PageData<TEntity> pageData = new PageData<TEntity>
            {
                Totals = await data.CountAsync(),
                Rows = await data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
            return pageData;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <param name="ordering">排序条件（一定要有，多个用逗号隔开，倒序开头用-号）</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public PageData<TEntity> GetPage(Expression<Func<TEntity, bool>> whereLambda, string ordering, int pageIndex, int pageSize, bool isNoTracking = true)
        {
            // 分页 一定注意： Skip 之前一定要 OrderBy
            if (string.IsNullOrEmpty(ordering))
            {
                ordering = nameof(TEntity) + "Id";//默认以Id排序
            }
            var data = _dbSet.OrderByBatch(ordering);
            if (whereLambda != null)
            {
                data = isNoTracking ? data.Where(whereLambda).AsNoTracking() : data.Where(whereLambda);
            }
            PageData<TEntity> pageData = new PageData<TEntity>
            {
                Totals = data.Count(),
                Rows = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
            };
            return pageData;
        }
        #endregion
    }
}