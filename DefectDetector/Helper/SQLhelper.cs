using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Helper
{
    public class SQLhelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 检查数据库连接是否可用
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, int cmdType, params SqlParameter[] parameters)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //执行脚本的对象cmd
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                result = cmd.ExecuteNonQuery();//执行T-SQL并返回受影响行数
                cmd.Parameters.Clear();
            }
            return result;
        }

        /// <summary>
        /// 执行sql查询，返回第一行第一列的值
        /// </summary>
        /// <param name="sql">sql语句或存储过程</param>
        /// <param name="cmdType">执行的脚本类型 1:sql语句  2:存储过程</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, int cmdType, params SqlParameter[] parameters)
        {
            object result = null;//返回结果
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //执行脚本的对象cmd
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                result = cmd.ExecuteScalar();//执行T-SQL并返回第一行第一列的值
                cmd.Parameters.Clear();
                if (result == null || result == DBNull.Value)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// 执行sql查询,返回SqlDataReader对象
        /// </summary>
        /// <param name="sql">sql语句或存储过程</param>
        /// <param name="cmdType">执行的脚本类型 1:sql语句  2:存储过程</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, int cmdType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception("创建reader对象发生异常", ex);
            }

        }


        /// <summary>
        /// 构建SqlCommand
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="sql">SQL语句或存储过程</param>
        /// <param name="comType">命令字符串的类型</param>
        /// <param name="trans">事务</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        private static SqlCommand BuilderCommand(SqlConnection conn, string sql, int cmdType, SqlTransaction trans, params SqlParameter[] paras)
        {
            if (conn == null) throw new ArgumentNullException("连接对象不能为空！");
            SqlCommand command = new SqlCommand(sql, conn);
            if (cmdType == 2)
                command.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            if (trans != null)
                command.Transaction = trans;
            if (paras != null && paras.Length > 0)
            {
                command.Parameters.Clear();
                command.Parameters.AddRange(paras);
            }
            return command;
        }


    }
}
