using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices.Base;
using Domain;
using System.Linq.Expressions;
using ViewModel;

namespace IServices
{
    public interface IAdminServices : IBaseServices<Admin>
    {
        Tuple<IList<Admin>, int> GetListByPage(AdminQuery query, Expression<Func<Admin, int>> keySelector, int pageIndex = 1, int pageSize = 10);
    }
}
