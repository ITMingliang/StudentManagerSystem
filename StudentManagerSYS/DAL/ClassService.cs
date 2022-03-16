using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 班级信息服务类
    /// </summary>
    public  class ClassService
    {
        //private static readonly string connString = "Server=PC-201902191642\\MSSQLSERVER20128;DataBase=StudentDB;Uid=sa;Pwd=ant123";

        #region 查询所有班级

        /// <summary>
        /// 查询所有班级
        /// </summary>
        /// <returns></returns>
        public List<StudentClass> GetAllClass()
        {
            string sql = "select ClassId, ClassName from StudentClass";

            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<StudentClass> students = new List<StudentClass>();
            while (reader.Read())
            {
                StudentClass Stu = new StudentClass()
                {
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()

                };
                students.Add(Stu);
            }
            reader.Close();
            return students;
        }


        ///// <summary>
        ///// 查询所有班级
        ///// </summary>
        ///// <returns></returns>
        //public List<StudentClass> GetAllClass()
        //{
        //    string sql = "select ClassId, ClassName from StudentClass";
        //    SqlConnection conn = new SqlConnection(connString);//准备好数据库链接
        //    SqlCommand cmd = new SqlCommand(sql, conn);//准备发送命令
        //    conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    List<StudentClass> students = new List<StudentClass>();
        //    while (reader.Read())
        //    {
        //        StudentClass Stu=  new StudentClass()
        //        {
        //            ClassId = Convert.ToInt32(reader["ClassId"]),
        //            ClassName = reader["ClassName"].ToString()

        //        };
        //        students.Add(Stu);
        //     }
        //    return students;
        //}


        #endregion
    }
}
