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
    /// 成绩数据服务类
    /// </summary>
   public class ScoreService
    {

        //private static readonly string connString = @"Server=PC-201902191642\MSSQLSERVER20128;DataBase=StudentDB;Uid=sa;Pwd=ant123";

        #region 考试信息

        /// <summary>
        /// 考试信息
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="classId"></param>
        /// <param name="stuName"></param>
        /// <returns></returns>
        public List<ScoreList> GetScoreLists(int? studentId,int? classId,string stuName)
        {

            #region SQL语句1

            string sql = $"select Students.StudentId, IdentityNO, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, Students.ClassId,ClassName,Students.StudentId, SQLServer, CSharp, HTMLCSS, UpdateTime from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId inner join ScoreList on ScoreList.StudentId = Students.StudentId ";

            if (studentId != 0)
            {
                sql += $" where Students.StudentId={studentId}";
            }
            else if (classId != 0)
            {
                sql += $" where Students.ClassId={classId}";
            }
            else if (stuName.Length > 0)
            {
                sql += $" where StudentName  like '%{stuName}%'";
            }



            #endregion

            #region SQL语句1

            //string sql = "";

            //if (studentId!=0)
            //{
            //     sql += $"select Students.StudentId, IdentityNO, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, Students.ClassId,ClassName,Students.StudentId, SQLServer, CSharp, HTMLCSS, UpdateTime from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId inner join ScoreList on ScoreList.StudentId = Students.StudentId where Students.StudentId={studentId}";
            //}else if (classId != 0)
            //{
            //    sql += $"select Students.StudentId, IdentityNO, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, Students.ClassId,ClassName,Students.StudentId, SQLServer, CSharp, HTMLCSS, UpdateTime from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId inner join ScoreList on ScoreList.StudentId = Students.StudentId where Students.ClassId={classId}";
            //}
            //else if (stuName.Length>0)
            //{
            //    sql += $"select Students.StudentId, IdentityNO, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, Students.ClassId,ClassName,Students.StudentId, SQLServer, CSharp, HTMLCSS, UpdateTime from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId inner join ScoreList on ScoreList.StudentId = Students.StudentId where StudentName  like '%{stuName}%'";
            //}
            //else
            //{
            //    sql += $"select Students.StudentId, IdentityNO, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, Students.ClassId,ClassName,Students.StudentId, SQLServer, CSharp, HTMLCSS, UpdateTime from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId inner join ScoreList on ScoreList.StudentId = Students.StudentId";
            //}

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    conn.Open();
            //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    List<ScoreList> list = new List<ScoreList>();
            //    while (reader.Read())
            //    {
            //        ScoreList scoreList = new ScoreList()
            //        {
            //            StudentId = Convert.ToInt32(reader["StudentId"]),
            //            IdentityNO = reader["IdentityNO"].ToString(),
            //            StudentName = reader["StudentName"].ToString(),
            //            Gender = reader["Gender"].ToString(),
            //            Birthday = Convert.ToDateTime(reader["Birthday"]),
            //            AttendanceNO = reader["AttendanceNO"].ToString(),
            //            StuImage = reader["StuImage"].ToString(),
            //            Age = Convert.ToInt32(reader["Age"]),
            //            Phone = reader["Phone"].ToString(),
            //            StudentAddress = reader["StudentAddress"].ToString(),
            //            ClassId = Convert.ToInt32(reader["ClassId"]),
            //            ClassName = reader["ClassName"].ToString(),
            //            SQLServer = reader["SQLServer"].ToString(),
            //            CSharp = reader["CSharp"].ToString(),
            //            HTMLCSS = reader["HTMLCSS"].ToString(),
            //            UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
            //        };
            //        list.Add(scoreList);
            //    }
            //    return list;
            //}


            #endregion

                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<ScoreList> list = new List<ScoreList>();
                while (reader.Read())
                {
                    ScoreList scoreList= new ScoreList()
                    {
                        StudentId = Convert.ToInt32(reader["StudentId"]),
                        IdentityNO = reader["IdentityNO"].ToString(),
                        StudentName = reader["StudentName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Birthday = Convert.ToDateTime(reader["Birthday"]),
                        AttendanceNO = reader["AttendanceNO"].ToString(),
                        StuImage = reader["StuImage"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Phone = reader["Phone"].ToString(),
                        StudentAddress = reader["StudentAddress"].ToString(),
                        ClassId = Convert.ToInt32(reader["ClassId"]),
                        ClassName = reader["ClassName"].ToString(),
                        SQLServer = reader["SQLServer"].ToString(),
                        CSharp = reader["CSharp"].ToString(),
                        HTMLCSS = reader["HTMLCSS"].ToString(),
                        UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
                    };
                    list.Add(scoreList);
                }
                reader.Close();
                return list;
            
        }

        #endregion

        #region 获取考试统计信息

        /// <summary>
        /// 获取考试统计信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetScoreInfo()
        {
            string sql = "select  stuCount= count(*),avgSQL=AVG(SQLServer),avgHTML=AVG(HTMLCSS) from ScoreList select absentCount=count(*) from Students where StudentId not in (select studentId from ScoreList)";

            SqlDataReader reader = SQLHelper.GetReader(sql);
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                if (reader.Read())
                {
                    Dic.Add("stuCount", reader["stuCount"].ToString());
                    Dic.Add("avgSQL", reader["avgSQL"].ToString());
                    Dic.Add("avgHTML", reader["avgHTML"].ToString());
                }
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        Dic.Add("absentCount", reader["absentCount"].ToString());
                    }
                }
              reader.Close();
                return Dic;
            

        }



        ///// <summary>
        ///// 获取考试统计信息
        ///// </summary>
        ///// <returns></returns>
        //public Dictionary<string, string> GetScoreInfo()
        //{
        //    string sql = "select  stuCount= count(*),avgSQL=AVG(SQLServer),avgHTML=AVG(HTMLCSS) from ScoreList select absentCount=count(*) from Students where StudentId not in (select studentId from ScoreList)";
        //    using(SqlConnection conn=new SqlConnection(connString))
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //       SqlDataReader reader= cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        Dictionary<string, string> Dic = new Dictionary<string, string>();
        //        if (reader.Read())
        //        {
        //            Dic.Add("stuCount", reader["stuCount"].ToString());
        //            Dic.Add("avgSQL", reader["avgSQL"].ToString());
        //            Dic.Add("avgHTML", reader["avgHTML"].ToString());
        //        }
        //        if (reader.NextResult())
        //        {
        //            if (reader.Read())
        //            {
        //                Dic.Add("absentCount", reader["absentCount"].ToString());
        //            }
        //        }
        //        return Dic;
        //    }

        // }

        #endregion

        #region 统计缺考人员名单


        /// <summary>
        /// 统计缺考人员名单
        /// </summary>
        /// <returns></returns>
        public List<string> GetAbsentList()
        {
            string sql = "select StudentName from Students where StudentId not in(select StudentId from ScoreList)";

            SqlDataReader reader = SQLHelper.GetReader(sql);
                List<string> list = new List<string>(); ;
                while (reader.Read())
                {
                    list.Add(reader["StudentName"].ToString());
                }
            
            
                reader.Close();
                return list;

        }

        ///// <summary>
        ///// 统计缺考人员名单
        ///// </summary>
        ///// <returns></returns>
        //public List<string> GetAbsentList()
        //{
        //    string sql = "select StudentName from Students where StudentId not in(select StudentId from ScoreList)";
        //    using (SqlConnection conn=new SqlConnection(connString))
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //       SqlDataReader reader= cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        List<string> list = new List<string>(); ;
        //        while (reader.Read())
        //        {
        //            list.Add(reader["StudentName"].ToString());
        //        }
        //        return list;

        //    }
        //}
        #endregion

    }
}
