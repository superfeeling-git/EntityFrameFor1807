using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class AdminModel
    {
        public int AdminID { get; set; }
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }    //可空
        public string LastLoginIP { get; set; }
    }
}
