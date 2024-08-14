using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多语言支持04
{
    internal class UserScoresT1Operation
    {
        public static List<UserScoresT1Model> GetScoreByUsersName(string usersName)
        {
            List<UserScoresT1Model> userScores = new List<UserScoresT1Model>();
            string sql = $"select *from UserScoresT1 where UsersName='{usersName}'";
            DataTable table = SelectData(sql);
            for (int i = 0; i<table.Rows.Count; i++)
            {
                UserScoresT1Model model = loopRightNullOrEmpty(table, i);
                userScores.Add(model);
            }
            return userScores;
        }
        /// <summary>
        /// 取 表UserScoresT1方法
        /// </summary>
        public static DataTable SelectData(string sql)//    返回得到的表
        {
            SqlConnection scoon = new SqlConnection("Server=localhost;Database=Text;Trusted_Connection=true");
            scoon.Open();
            SqlCommand cmd = new SqlCommand(sql, scoon);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            scoon.Close();

            DataTable table = new DataTable();
            table=ds.Tables[0];
            return table;
        }
        /// <summary>
        /// 存 表UserScoresT1方法
        /// </summary>
        public int Edit(string sql)//   返回受影响的行数
        {
            SqlConnection scoon = new SqlConnection("Server=localhost;Database=Text;Trusted_Connection=true");
            scoon.Open();
            SqlCommand cmd = new SqlCommand(sql, scoon);
            int count = 0;//  受影响的行数
            try
            {
                count=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                count--;
                Console.WriteLine(ex);
            }
            scoon.Close();
            return count;
        }
        public static UserScoresT1Model loopRightNullOrEmpty(DataTable table,int i)
        {
            UserScoresT1Model model = new UserScoresT1Model();
            if (!string.IsNullOrEmpty(table.Rows[i]["UsersName"].ToString()))// 按列还能来个循环
                model.UsersName=table.Rows[i]["UsersName"].ToString();
            if (!string.IsNullOrEmpty(table.Rows[i]["Chinese"].ToString()))
                model.Chinese=Convert.ToSingle(table.Rows[i]["Chinese"]);
            if (!string.IsNullOrEmpty(table.Rows[i]["English"].ToString()))
                model.English= Convert.ToSingle(table.Rows[i]["English"]);//(float)table.Rows[i]["English"];// 无法强制转换
            if (!string.IsNullOrEmpty(table.Rows[i]["Math"].ToString()))
                model.Math=float.Parse(table.Rows[i]["Math"].ToString());
            if (!string.IsNullOrEmpty(table.Rows[i]["RecordTime"].ToString()))
                model.RecordTime=DateTime.Parse(table.Rows[i]["RecordTime"].ToString()); //(DateTime)table.Rows[i]["RecordTime"];// 可能会有异常
            return model;
        }
    }
}
