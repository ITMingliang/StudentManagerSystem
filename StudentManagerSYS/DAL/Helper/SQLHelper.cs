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
    /// SQL帮助类--用来提供我们对数据库的访问
    /// </summary>
   public static  class SQLHelper
    {
        private static readonly string connString = @"Server=PC-201902191642\MSSQLSERVER20128;DataBase=StudentDB;Uid=sa;Pwd=ant123";

        #region 增、删、 改

        /// <summary>
        /// 增、删、 改
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static bool Update (string sqlString)
        {
            using (SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        #endregion

        #region 执行多结果查询

        /// <summary>
        /// 执行多结果查询
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sqlString)
        {
            ////这是错误代码 
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    SqlCommand cmd = new SqlCommand(sqlString, conn);
            //    conn.Open();
            //    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //}

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region 执行单 一结果查询
        /// <summary>
        /// 执行单 一结果查询
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sqlString)
        {
            using (SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                conn.Open();
               return cmd.ExecuteScalar();
            }
        }
        #endregion

        #region 执行事务
        /// <summary>
        /// 事务就是一一系列SQL语句执行，要么全部成功，要么失败
        /// </summary>
        /// <param name="sqlList">sql语句List</param>
        /// <returns></returns>
        public static bool UpdateByTransaction(List<string> sqlList)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();//【1】开启事务
                foreach (string sql in sqlList)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();//【2】提交事务（同时还要自动清除事务）
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction!=null)
                {
                    cmd.Transaction.Rollback();//【3】回滚事务（同时还要自动清除事务）
                }
                throw new Exception("调用UpdateByTransaction(List<string> sqlList)方法出错!"+ex.Message);
            }
            finally
            {
                if (cmd.Transaction!=null)
                {
                    cmd.Transaction = null;
                }
                conn.Close();
            }
        }

        #endregion
    }
}
