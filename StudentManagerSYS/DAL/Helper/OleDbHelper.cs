using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Oledb数据源帮助类--用来提供我们对数据库的访问
    /// Access数据库、Excel文档他都是可以读取
    /// </summary>
   public static  class OleDbHelper
    {

        private static string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0";

        //链接数据Access数据库
        //string static string connAccess = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\aaa.accdb;Persist Security Info=False;";

        #region 增、删、 改

        /// <summary>
        /// 增、删、 改
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static bool Update (string sqlString)
        {
            using (OleDbConnection conn=new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand(sqlString, conn);
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
        public static OleDbDataReader GetReader(string sqlString)
        {

                OleDbConnection conn = new OleDbConnection(connString);
                OleDbCommand cmd = new OleDbCommand(sqlString, conn);
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
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand(sqlString, conn);
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
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand();
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

        #region 读取数据集对象
        /// <summary>
        /// 读取数据集对象
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql,string path)
        {
            OleDbConnection conn = new OleDbConnection(string.Format(connString, path));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter dpt = new OleDbDataAdapter(cmd);//创建数适配器对象
            DataSet ds = new DataSet();//创建内存数据集
            try
            {
                conn.Open();
                dpt.Fill(ds);//使用数据适配器的Fill方法填充数据集
                return ds;//返回数据集

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
