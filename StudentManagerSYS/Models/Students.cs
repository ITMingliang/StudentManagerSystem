using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 学生实体类（做数据传输--UI界面---》数据库   |    从数据库读取---》UI界面）
    /// </summary>
    public  class Students
    {
        /// <summary>
        /// 学号
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string    StudentName { get; set; }
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
        public string    StudentAddress { get; set; }

        //扩展属性（特殊情况下添加额外的属性）
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
