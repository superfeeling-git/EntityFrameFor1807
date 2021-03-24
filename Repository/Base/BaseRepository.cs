using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IRepository;
using Z.EntityFramework;
using Z.EntityFramework.Extensions;

namespace Repository.Base
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class, new()
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Create(TEntity entity)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                db.Set<TEntity>().Add(entity);

                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(TEntity entity)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                await db.Set<TEntity>().SingleInsertAsync(entity);
            }
        }
        #endregion

        #region 三种删除
        /// <summary>
        /// 传递拉姆达表达式删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().Where(predicate).DeleteFromQuery();
            }
        }

        /// <summary>
        /// 按主键ID单条删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(int id)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().DeleteByKey(id);
            }
        }

        /// <summary>
        /// 按主键ID批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(int[] idList)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().DeleteByKey(idList);
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().Where(predicate).UpdateFromQuery(updateExpression);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据主键查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Find(int id)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().Find(id);
            }
        }

        /// <summary>
        /// 根据条件获取单条实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().FirstOrDefault(predicate);
            }
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().ToList();
            }
        }

        /// <summary>
        /// 根据条件查询多条实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().Where(predicate).ToList();
            }
        }

        /// <summary>
        /// 根据分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Tuple<IList<TEntity>, int> GetListByPage(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> keySelector, int pageIndex = 1, int pageSize = 10)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                Tuple<IList<TEntity>, int> tuple = new Tuple<IList<TEntity>, int>
                    (
                    item1: db.Set<TEntity>().OrderBy(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                    item2: db.Set<TEntity>().Count()
                    );

                return tuple;
            }
        }
        #endregion
    }
}
