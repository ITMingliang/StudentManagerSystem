using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 管理员用户类
    /// </summary>
   public class SysAdmin
    {
        /// <summary>
        /// 用户登陆ID
        /// </summary>
        public int LoginId { get; set; }//属性本质是在内存里开辟一个空间

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }
    }
}
