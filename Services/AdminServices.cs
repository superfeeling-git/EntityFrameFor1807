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
    }
}
