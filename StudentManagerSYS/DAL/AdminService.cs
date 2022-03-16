
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 管理员数据访问类
    /// </summary>
   public class AdminService
    {
        //private static readonly string connString = "Server='PC-201902191642\\MSSQLSERVER20128';DataBase='StudentDB';Uid='sa';Pwd='ant123'";


        #region 根据帐号密码查询管理员对象
        /// <summary>
        /// 根据帐号密码查询管理员对象
        /// </summary>
        /// <param name="sysAdmin"></param>
        /// <returns></returns>
        public SysAdmin GetAdmin(SysAdmin sysAdmin)
        {
            string sqlString = $"select AdminName,LoginId,Pwd from SyAdmin where LoginId={sysAdmin.LoginId} and Pwd={sysAdmin.LoginPwd}";//sql语句
               //方法一string.Format();
              //方法二StringBuilder                  
            SqlDataReader reader = SQLHelper.GetReader(sqlString);
                //（CommandBehavior.CloseConnection的意思就是查询完数据后自己关闭）

            //创建SysAdmin对象
            SysAdmin Admin = new SysAdmin();
            if (reader.Read())//循环读取数据
            {
                //转换成SysAdmin对象
                Admin.AdminName = reader["AdminName"].ToString();
                Admin.LoginId = Convert.ToInt32(reader["LoginId"]);
                Admin.LoginPwd = reader["Pwd"].ToString();

            }
            reader.Close();//关闭SqlDataReader对象
            return Admin;
        }


        ///// <summary>
        ///// 根据帐号密码查询管理员对象
        ///// </summary>
        ///// <param name="sysAdmin"></param>
        ///// <returns></returns>
        //public SysAdmin GetAdmin(SysAdmin sysAdmin)
        //{
        //    string sqlString= $"select AdminName,LoginId,Pwd from SyAdmin where LoginId={sysAdmin.LoginId} and Pwd={sysAdmin.LoginPwd}";//sql语句
        //    //方法一string.Format();
        //    //方法二StringBuilder

        //    SqlConnection conn = new SqlConnection(connString);//链接到数据库
        //    SqlCommand cmd = new SqlCommand(sqlString, conn);//发送命令到数据库
        //    conn.Open();//打开数据库链接
        //     SqlDataReader reader=  cmd.ExecuteReader(CommandBehavior.CloseConnection);//查询数据（CommandBehavior.CloseConnection的意思就是查询完数据后自己关闭）

        //    //创建SysAdmin对象
        //    SysAdmin Admin =new SysAdmin();
        //    if (reader.Read())//循环读取数据
        //    {
        //        //转换成SysAdmin对象
        //        Admin.AdminName = reader["AdminName"].ToString();
        //        reader.Close();//关闭SqlDataReader对象
        //    }

        //    return Admin;
        //}

        #endregion

        #region 管理员密码修改
        /// <summary>
        /// 管理员密码修改
        /// </summary>
        /// <param name="sysAdmin">管理员对象</param>
        /// <returns></returns>
        public bool ModifyPwd(SysAdmin sysAdmin)
        {
            string sql = $"update SyAdmin set Pwd={sysAdmin.LoginPwd} where LoginId={sysAdmin.LoginId}";
           return SQLHelper.Update(sql);
        }
        #endregion
    }
}
