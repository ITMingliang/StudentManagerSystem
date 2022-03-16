using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{

    /// <summary>
    /// 学生服务类
    /// </summary>
   public class StudentService
    {
        //private static readonly string connString = @"Server=PC-201902191642\MSSQLSERVER20128;DataBase=StudentDB;Uid=sa;Pwd=ant123";

        #region 添加学生到数据库

        /// <summary>
        /// 添加学生到数据库
        /// </summary>
        /// <param name="students">学生对象</param>
        /// <returns></returns>
        public bool AddStudnet(Students students)
        {

            string sqlString = $"insert  into Students(StudentName, Gender  , Birthday  , AttendanceNO , StuImage , Age , Phone , StudentAddress,ClassId,IdentityNO )VALUES('{students.StudentName}', '{students.Gender}', '{students.Birthday}', '{students.AttendanceNO}', '{students.StuImage}', {students.Age}, '{students.Phone}', '{students.StudentAddress}',{students.ClassId},'{students.IdentityNO}')";
              return  SQLHelper.Update(sqlString);
           
        }

        ///// <summary>
        ///// 添加学生到数据库
        ///// </summary>
        ///// <param name="students">学生对象</param>
        ///// <returns></returns>
        //public int AddStudnet(Students students)
        //{

        //    string sqlString = $"insert  into Students(StudentName, Gender  , Birthday  , AttendanceNO , StuImage , Age , Phone , StudentAddress,ClassId,IdentityNO )VALUES('{students.StudentName}', '{students.Gender}', '{students.Birthday}', '{students.AttendanceNO}', '{students.StuImage}', {students.Age}, '{students.Phone}', '{students.StudentAddress}',{students.ClassId},'{students.IdentityNO}')";
        //    SqlConnection conn = new SqlConnection(connString);//链接到数据库
        //    SqlCommand cmd = new SqlCommand(sqlString, conn);//发送命令到数据库 
        //    conn.Open();//打开数据库链接
        //    int num= cmd.ExecuteNonQuery();//返回值 是一个数字，成功大于0 ，失败=0
        //    conn.Close();//关闭链接
        //    return num;

        //}

        #endregion

        #region 返回所有学生信息


        /// <summary>
        /// 返回所有学生信息
        /// </summary>
        /// <returns></returns>
        public List<Students> GetAllStudent()
        {
            string sql = "select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId";
             SqlDataReader reader= SQLHelper.GetReader(sql);
            List<Students> students = new List<Students>();
            while (reader.Read())
            {
                Students stu = new Students()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    AttendanceNO = reader["AttendanceNO"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                };
                students.Add(stu);
            }
            reader.Close();
            return students;
        }

        ///// <summary>
        ///// 返回所有学生信息
        ///// </summary>
        ///// <returns></returns>
        //public List<Students> GetAllStudent()
        //{
        //    string sql = "select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId";
        //    SqlConnection conn = new SqlConnection(connString);
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    List<Students> students = new List<Students>();
        //    while (reader.Read())
        //    {
        //        Students stu = new Students()
        //        {
        //            StudentId= Convert.ToInt32(reader["StudentId"]), 
        //            StudentName= reader["StudentName"].ToString(),
        //            Gender = reader["Gender"].ToString(), 
        //            Birthday = Convert.ToDateTime(reader["Birthday"]), 
        //            AttendanceNO = reader["AttendanceNO"].ToString(),
        //            StuImage = reader["StuImage"].ToString(), 
        //            Age = Convert.ToInt32(reader["Age"]), 
        //            Phone = reader["Phone"].ToString(), 
        //            StudentAddress = reader["StudentAddress"].ToString(),
        //            ClassId=Convert.ToInt32(reader["ClassId"]),
        //            ClassName = reader["ClassName"].ToString()
        //        };
        //        students.Add(stu);
        //    }

        //    return students;
        //}


        #endregion

        #region 根据学生ID查询详细信息


        /// <summary>
        /// 根据学生ID查询详细信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Students GetStudentDetail(string studentId)
        {
            string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where StudentId={studentId}";
            SqlDataReader reader= SQLHelper.GetReader(sql);
            Students students = new Students();
            if (reader.Read())
            {
                students = new Students()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    IdentityNO = reader["IdentityNO"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    AttendanceNO = reader["AttendanceNO"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                };
            }
            reader.Close();
            return students;
        }

        ///// <summary>
        ///// 根据学生ID查询详细信息
        ///// </summary>
        ///// <param name="studentId">学生ID</param>
        ///// <returns></returns>
        //public Students GetStudentDetail(string studentId)
        //{
        //    string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where StudentId={studentId}";
        //    SqlConnection conn = new SqlConnection(connString);
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    Students students = new Students();
        //    if (reader.Read())
        //    {
        //        students = new Students()
        //        {
        //            StudentId = Convert.ToInt32(reader["StudentId"]),
        //            StudentName = reader["StudentName"].ToString(),
        //            IdentityNO = reader["IdentityNO"].ToString(),
        //            Gender = reader["Gender"].ToString(),
        //            Birthday = Convert.ToDateTime(reader["Birthday"]),
        //            AttendanceNO = reader["AttendanceNO"].ToString(),
        //            StuImage = reader["StuImage"].ToString(),
        //            Age = Convert.ToInt32(reader["Age"]),
        //            Phone = reader["Phone"].ToString(),
        //            StudentAddress = reader["StudentAddress"].ToString(),
        //            ClassId = Convert.ToInt32(reader["ClassId"]),
        //            ClassName = reader["ClassName"].ToString()
        //        };
        //    }

        //    return students;
        //}


        #endregion

        #region 根据学生姓名模糊查询


        /// <summary>
        /// 根据学生姓名模糊查询
        /// </summary>
        /// <param name="studentName">学生姓名</param>
        /// <returns></returns>
        public Students GetStudentListById(string studentName)
        {
            string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where StudentName={studentName}";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            Students students = new Students();
            if (reader.Read())
            {
                students = new Students()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    IdentityNO = reader["IdentityNO"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    AttendanceNO = reader["AttendanceNO"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                };
            }
            reader.Close();
            return students;
        }

        ///// <summary>
        ///// 根据学生姓名模糊查询
        ///// </summary>
        ///// <param name="studentName">学生姓名</param>
        ///// <returns></returns>
        //public Students GetStudentListById(string studentName)
        //{
        //    string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where StudentName={studentName}";
        //    SqlConnection conn = new SqlConnection(connString);
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    Students students = new Students();
        //    if (reader.Read())
        //    {
        //        students = new Students()
        //        {
        //            StudentId = Convert.ToInt32(reader["StudentId"]),
        //            StudentName = reader["StudentName"].ToString(),
        //            IdentityNO = reader["IdentityNO"].ToString(),
        //            Gender = reader["Gender"].ToString(),
        //            Birthday = Convert.ToDateTime(reader["Birthday"]),
        //            AttendanceNO = reader["AttendanceNO"].ToString(),
        //            StuImage = reader["StuImage"].ToString(),
        //            Age = Convert.ToInt32(reader["Age"]),
        //            Phone = reader["Phone"].ToString(),
        //            StudentAddress = reader["StudentAddress"].ToString(),
        //            ClassId = Convert.ToInt32(reader["ClassId"]),
        //            ClassName = reader["ClassName"].ToString()
        //        };
        //    }

        //    return students;
        //}

        #endregion

        #region 根据学生姓名查询

        /// <summary>
        /// 根据学生姓名查询
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public List<Students> GetStudentListByName(string studentName)
        {
            string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where StudentName like '%{studentName}%'";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<Students> students = new List<Students>();
            while (reader.Read())
            {
                Students stu = new Students()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    IdentityNO = reader["IdentityNO"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    AttendanceNO = reader["AttendanceNO"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                };
                students.Add(stu);
            }
            reader.Close();
            return students;
        }

        #endregion

        #region 根据班级ID查询

        /// <summary>
        /// 根据班级ID查询
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<Students> GetStudentListByClassId(string classId)
        {
            string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where Students.ClassId={classId}";

            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<Students> students = new List<Students>();
            while (reader.Read())
            {
                Students stu = new Students()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    IdentityNO = reader["IdentityNO"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    AttendanceNO = reader["AttendanceNO"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    ClassId = Convert.ToInt32(reader["ClassId"]),
                    ClassName = reader["ClassName"].ToString()
                };
                students.Add(stu);
            }
            reader.Close();
            return students;
        }


        ///// <summary>
        ///// 根据班级ID查询
        ///// </summary>
        ///// <param name="classId"></param>
        ///// <returns></returns>
        //public List<Students> GetStudentListByClassId(string classId)
        //{
        //    string sql = $"select StudentId, StudentName, Gender, Birthday, AttendanceNO, StuImage, Age, Phone, StudentAddress,IdentityNO, StudentClass.ClassId,ClassName from Students inner join StudentClass on StudentClass.ClassId = Students.ClassId where Students.ClassId={classId}";
        //    SqlConnection conn = new SqlConnection(connString);
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    List<Students> students = new List<Students>();
        //    while (reader.Read())
        //    {
        //        Students stu = new Students()
        //        {
        //            StudentId = Convert.ToInt32(reader["StudentId"]),
        //            StudentName = reader["StudentName"].ToString(),
        //            IdentityNO = reader["IdentityNO"].ToString(),
        //            Gender = reader["Gender"].ToString(),
        //            Birthday = Convert.ToDateTime(reader["Birthday"]),
        //            AttendanceNO = reader["AttendanceNO"].ToString(),
        //            StuImage = reader["StuImage"].ToString(),
        //            Age = Convert.ToInt32(reader["Age"]),
        //            Phone = reader["Phone"].ToString(),
        //            StudentAddress = reader["StudentAddress"].ToString(),
        //            ClassId = Convert.ToInt32(reader["ClassId"]),
        //            ClassName = reader["ClassName"].ToString()
        //        };
        //        students.Add(stu);
        //    }

        //    return students;
        //}

        #endregion

        #region 修改学生信息

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="students">学生对象</param>
        /// <returns></returns>
        public bool UpdateStudentInfo(Students students)
        {
            string sql = $"update Students set  IdentityNO='{students.IdentityNO}', StudentName='{students.StudentName}', Gender='{students.Gender}', Birthday='{students.Birthday}', AttendanceNO='{students.AttendanceNO}', StuImage='{students.StuImage}', Age ={students.Age}, Phone ='{students.Phone}', StudentAddress ='{students.StudentAddress}', ClassId ={students.ClassId} where StudentId ={students.StudentId} ";
            return SQLHelper.Update(sql);
        }

        ///// <summary>
        ///// 修改学生信息
        ///// </summary>
        ///// <param name="students">学生对象</param>
        ///// <returns></returns>
        //public bool UpdateStudentInfo(Students students)
        //{
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        string sql = $"update Students set  IdentityNO='{students.IdentityNO}', StudentName='{students.StudentName}', Gender='{students.Gender}', Birthday='{students.Birthday}', AttendanceNO='{students.AttendanceNO}', StuImage='{students.StuImage}', Age ={students.Age}, Phone ='{students.Phone}', StudentAddress ='{students.StudentAddress}', ClassId ={students.ClassId} where StudentId ={students.StudentId} ";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //       int num= cmd.ExecuteNonQuery();
        //        conn.Close();
        //        if (num > 0) 
        //            return true;
        //        else 
        //            return false;
        //    }
        //}

        #endregion

        #region 根据ID删除学生信息

        /// <summary>
        /// 根据ID删除学生信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public bool DeleteStudentInfo(string studentId)
        {
            string sql = $"delete from Students where StudentId={studentId}";
            return SQLHelper.Update(sql);
        }

        ///// <summary>
        ///// 根据ID删除学生信息
        ///// </summary>
        ///// <param name="studentId">学生ID</param>
        ///// <returns></returns>
        //public bool DeleteStudentInfo(string studentId)
        //{
        //    using (SqlConnection conn=new SqlConnection(connString))
        //    {
        //        string sql = $"delete from Students where StudentId={studentId}";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //        int num= cmd.ExecuteNonQuery();
        //        conn.Close();
        //        if (num > 0) return true;
        //        else return false;
        //    }
        //}
        #endregion

    }


}
