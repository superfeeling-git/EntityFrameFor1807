using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRepository.Base;
using Domain;

namespace IRepository
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
    }
}
