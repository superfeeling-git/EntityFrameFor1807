using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using IRepository.Base;
using IServices.Base;

namespace Services.Base
{
    /// <summary>
    /// 基础抽象服务类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        private IBaseRepository<TEntity> BaseRepository;

        public BaseServices(IBaseRepository<TEntity> _BaseRepository)
        {
            this.BaseRepository = _BaseRepository;
        }

        #region 添加
        public int Create(TEntity entity)
        {
            return BaseRepository.Create(entity);
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
            return BaseRepository.Delete(predicate);
        }

        /// <summary>
        /// 按主键ID单条删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(int id)
        {
            return BaseRepository.Delete(id);
        }

        /// <summary>
        /// 按主键ID批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(int[] idList)
        {
            return BaseRepository.Delete(idList);
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
            return BaseRepository.Update(predicate, updateExpression);
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
            return BaseRepository.Find(id);
        }

        /// <summary>
        /// 根据条件获取单条实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return BaseRepository.Find(predicate);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            return BaseRepository.GetAll();
        }

        /// <summary>
        /// 根据条件查询多条实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            BinaryExpression exp = predicate.Body as BinaryExpression;

            var left = exp.Left;

            var right = exp.Right;

            if(right != null)
            {                
                string value = Expression.Lambda(left).Compile().DynamicInvoke().ToString();
            }

            return BaseRepository.GetList(predicate);
        }

        /// <summary>
        /// 根据分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Tuple<IList<TEntity>, int> GetListByPage(Expression<Func<TEntity, int>> keySelector, int pageIndex = 1, int pageSize = 10,params Expression<Func<TEntity, bool>>[] predicate)
        {
            return BaseRepository.GetListByPage(keySelector, pageIndex, pageSize, predicate);
        }
        #endregion
    }
}
