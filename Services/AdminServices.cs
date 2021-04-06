using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices.Base;
using IRepository;
using Services.Base;
using Domain;
using IServices;
using System.Linq.Expressions;
using ViewModel;

namespace Services
{
    public class AdminServices : BaseServices<Admin>, IAdminServices
    {
        private IAdminRepository adminRepository;
        public AdminServices(IAdminRepository _adminRepository)
            :base(_adminRepository)
        {
            this.adminRepository = _adminRepository;
        }

        public Tuple<IList<Admin>, int> GetListByPage(AdminQuery query, Expression<Func<Admin, int>> keySelector, int pageIndex = 1, int pageSize = 10)
        {
            var list = adminRepository.GetQuery();

            if(!string.IsNullOrWhiteSpace(query.Keywords))
            {
                list = list.Where(m => m.UserName.Contains(query.Keywords));
            }

            if(query.StartTime != null)
            {
                list = list.Where(m => m.LastLoginTime > query.StartTime);
            }

            if (query.EndTime != null)
            {
                list = list.Where(m => m.LastLoginTime < query.EndTime);
            }

            return adminRepository.GetListByPage(list, keySelector, pageIndex, pageSize);
        }
    }
}