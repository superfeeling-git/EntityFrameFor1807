using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        int Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        int Delete(int id);
        int Delete(int[] idList);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(int id);
        IList<TEntity> GetAll();
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Tuple<IList<TEntity>, int> GetListByPage(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> keySelector, int pageIndex = 1, int pageSize = 10);
        int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression);
    }
}
