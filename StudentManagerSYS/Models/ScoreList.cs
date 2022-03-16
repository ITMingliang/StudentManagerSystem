using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class ScoreList
    {

        public int ScoreId { get; set; }
        public int StudentId { get; set; }
        public string SQLServer { get; set; }
        public string CSharp { get; set; }
        public string HTMLCSS { get; set; }
        public DateTime UpdateTime { get; set; }

        //扩展属性
        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

      
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityNO { get; set; }

        /// <summary>
        /// 考勤号
        /// </summary>
        public string AttendanceNO { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string StuImage { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string StudentAddress { get; set; }
    }
}
