using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IRepository;

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
                var mm =  db.Set<TEntity>().FirstOrDefault(predicate);

                db.Set<TEntity>().Remove(mm);

                db.Entry<TEntity>(mm).State = System.Data.Entity.EntityState.Deleted;

                db.SaveChanges();

                return 1;
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
                var Entity = db.Set<TEntity>().Find(id);
                db.Set<TEntity>().Remove(Entity);
                return db.SaveChanges();
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
        public virtual int Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                var Entity = db.Set<TEntity>().FirstOrDefault(predicate);

                if(Entity != null)
                {
                    db.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
                    return db.SaveChanges();
                }
                else
                {
                    return 0;
                }
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


        public virtual IQueryable<TEntity> GetQuery()
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                return db.Set<TEntity>().AsQueryable();
            }
        }

        public virtual Tuple<IList<TEntity>, int> GetListByPage(IQueryable<TEntity> entities, Expression<Func<TEntity, int>> keySelector, int pageIndex = 1, int pageSize = 10)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                Tuple<IList<TEntity>, int> tuple = new Tuple<IList<TEntity>, int>
                    (
                    item1: entities.OrderBy(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                    item2: entities.Count()
                    );

                return tuple;
            }
        }



        /// <summary>
        /// 根据分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Tuple<IList<TEntity>, int> GetListByPage(Expression<Func<TEntity, int>> keySelector, int pageIndex = 1, int pageSize = 10, params Expression<Func<TEntity, bool>>[] predicate)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                var list = db.Set<TEntity>().AsQueryable();
                foreach (var item in predicate)
                {
                    var nodeType = item.Body.NodeType;

                    if(nodeType == ExpressionType.Equal)
                    {
                        BinaryExpression binaryExpression = item.Body as BinaryExpression;

                        if(binaryExpression.Right != null)
                        {
                            list = list.Where(item);
                        }
                    }
                    if(nodeType == ExpressionType.Call)
                    {
                        MethodCallExpression methodCallExpression = item.Body as MethodCallExpression;

                        ConstantExpression constantExpression = methodCallExpression.Arguments[0] as ConstantExpression;

                        if(constantExpression.Value != null)
                        {
                            list = list.Where(item);
                        }
                    }

                }

                Tuple<IList<TEntity>, int> tuple = new Tuple<IList<TEntity>, int>
                    (
                    item1: list.OrderBy(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                    item2: list.Count()
                    );

                return tuple;
            }
        }
        #endregion
    }

    /// <summary>
    /// 支持本地变量的Expression
    /// </summary>
    public class LocalExpression
    {
        public static object GetValue<T>(Expression<Func<T>> func)
        {
            object value = new object();
            var body = func.Body;

            if (body.NodeType == ExpressionType.Constant)
            {
                value = ((ConstantExpression)body).Value;
            }
            else
            {
                var memberExpression = (MemberExpression)body;

                var @object =
                  ((ConstantExpression)(memberExpression.Expression)).Value; //这个是重点

                if (memberExpression.Member.MemberType == MemberTypes.Field)
                {
                    value = ((FieldInfo)memberExpression.Member).GetValue(@object);
                }
                else if (memberExpression.Member.MemberType == MemberTypes.Property)
                {
                    value = ((PropertyInfo)memberExpression.Member).GetValue(@object);
                }
            }
            return value;
        }
    }
}
