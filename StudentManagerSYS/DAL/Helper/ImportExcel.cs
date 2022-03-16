using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    //导入Excel
    public  class ImportExcel
    {
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public List<Students> GetStudentByExcel(string path)
        {
            List<Students> stuList = new List<Students>();
            DataSet ds = OleDbHelper.GetDataSet("select *from [Sheet1$]", path);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                Students stu = new Students()
                {
                    IdentityNO = row["身份证"].ToString(),
                    StudentName = row["学生姓名"].ToString(),
                    Gender = row["姓别"].ToString(),
                    Birthday = Convert.ToDateTime(row["生日"]),
                    AttendanceNO = row["考勤号"].ToString(),
                    StuImage = row["图片"].ToString(),
                    Age =Convert.ToInt32(row["年龄"]),
                    Phone = row["电话"].ToString(),
                    StudentAddress = row["地址"].ToString(),
                    ClassId = Convert.ToInt32(row["班级ID"]),
                };
                stuList.Add(stu);
            }
            return stuList;
        }

        /// <summary>
        /// 将集合中学生添加到数据库
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Import(List<Students> list)
        {
            List<string> sqlList = new List<string>();
            foreach (Students students in list)
            {
                string sql = $"insert  into Students(StudentName, Gender  , Birthday  , AttendanceNO , StuImage , Age , Phone , StudentAddress,ClassId,IdentityNO )VALUES('{students.StudentName}', '{students.Gender}', '{students.Birthday}', '{students.AttendanceNO}', '{students.StuImage}', {students.Age}, '{students.Phone}', '{students.StudentAddress}',{students.ClassId},'{students.IdentityNO}')";
                sqlList.Add(sql);
            }
            return SQLHelper.UpdateByTransaction(sqlList);
       
        } 
    }
}
