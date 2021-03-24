using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using IRepository.Base;
using Domain;
using IRepository;

namespace Repository
{
    /// <summary>
    /// 管理员的仓储
    /// </summary>
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {

    }
}
