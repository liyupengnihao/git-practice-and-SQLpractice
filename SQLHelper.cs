using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多语言支持04
{
    internal class SQLHelper
    {
        /// <summary>
        /// 存表UserT1数据方法
        /// </summary>
        /// <param name="sql"></param>
        public static int EditData(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString="Server=localhost;Database=Text;Trusted_Connection=true"; //  用windows就是true，要账户密码就要加过内容
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);

            int count = 0;
            try
            {
                count = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                count--;//  有异常count减一，无异常，且执行了当没有改变行count为0
                Console.WriteLine(ex);
            }
            conn.Close();
            return count;
        }
        /// <summary>
        /// 取表UserT1数据方法
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns></returns>
        public static DataTable SelectData(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString="Server=localhost;Database=Text;Trusted_Connection=true;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter();//    SqlDataAdapter填充DataSet的
            adapter.SelectCommand=cmd;

            DataSet ds = new DataSet();//   缓存区间
            adapter.Fill(ds);
            conn.Close();
            DataTable dt = new DataTable();
            dt=ds.Tables[0];
            return dt;

        }
    }
}
