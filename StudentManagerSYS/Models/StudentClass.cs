using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 学生班级实体类
    /// </summary>
   public class StudentClass
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
    }
}
