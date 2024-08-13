using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 多语言支持04
{
    internal class UserT1Operation
    {
        /// <summary>
        /// 行内容写入（我写）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <param name="i"></param>
        public static UserT1Model operation( DataTable table,int i)
        {
            UserT1Model model = new UserT1Model();
            model.UsersName=table.Rows[i]["UsersName"].ToString();// 还可以把这一步抽象为方法
            model.Password=table.Rows[i]["Password"].ToString();
            model.NickName=table.Rows[i]["NickName"].ToString();
            model.Gender=table.Rows[i]["Gender"].ToString();
            return model;
        }
        /// <summary>
        /// 与上面差不多的功能（视频写）
        /// </summary>
        public static UserT1Model DataRowToUserTModel(DataRow row)
        {
            UserT1Model model=new UserT1Model();
            model.UsersName=row["UsersName"].ToString();// 还可以把这一步抽象为方法
            model.Password=row["Password"].ToString();
            model.NickName=row["NickName"].ToString();
            model.Gender=row["Gender"].ToString();
            return model;
        }
        /// <summary>
        /// 登录方法(我)
        /// </summary>
        /// <param name="inputeName"></param>
        /// <param name="inputePwd"></param>
        /// <returns></returns>
        public static UserT1Model Login(string inputeName, string inputePwd)
        {
            string sql = $"select*from UserT1 where UsersName='{inputeName}'and Password='{inputePwd}'";
            DataTable table = SQLHelper.SelectData(sql);
            UserT1Model userTM=null;
            if (table.Rows.Count<=0)
            {
                Console.WriteLine("账号或密码不正确");
                return null;

            }
            else
            {
                userTM=UserT1Operation.operation(table, 0);//   用方法完成下面这个
                //  另一个相同作用的方法方法
                userTM =UserT1Operation.DataRowToUserTModel(table.Rows[0]);//   同一类下可省略UserT1Operation，如上面一个方法
                #region 方法实现的内容
                ////  怕数据库的属性写错
                //userTM.UsersName=table.Rows[0]["UsersName"].ToString();
                //userTM.Password=table.Rows[0]["Password"].ToString();
                //userTM.NickName=table.Rows[0]["NickName"].ToString();
                //userTM.Gender=table.Rows[0]["Gender"].ToString();
                #endregion
                return userTM;
            }
        }
        /// <summary>
        /// 查寻数据库中所有的数据(我)
        /// </summary>
        /// <returns></returns>
        public static List<UserT1Model> AllUsers()
        {
            string sql = "select*from UserT1";
            Console.WriteLine("用户名 昵称 性别");
            DataTable tableTwo = SQLHelper.SelectData(sql);
            List<UserT1Model> users = new List<UserT1Model>();
            for (int i = 0; i<tableTwo.Rows.Count; i++)
            {
                UserT1Model model = new UserT1Model();
                model=UserT1Operation.operation(tableTwo, i);//  用方法完成下面这个步骤
                //  另一个相同作用的方法方法
                model=UserT1Operation.DataRowToUserTModel(tableTwo.Rows[i]);
                #region 方法实现的内容
                //model.UsersName=tableTwo.Rows[i]["UsersName"].ToString();// 还可以把这一步抽象为方法
                //model.Password=tableTwo.Rows[i]["Password"].ToString();
                //model.NickName=tableTwo.Rows[i]["NickName"].ToString();
                //model.Gender=tableTwo.Rows[i]["Gender"].ToString();
                #endregion
                users.Add(model);
            }
            return users;
        }
        /// <summary>
        /// 填入性别(我)
        /// </summary>
        public static void UPDataGender(UserT1Model userTM)
        {
            while (true)
            {
                Console.WriteLine("请输入1：男 2：女 3:未知性别");
                string inputeGender = Console.ReadLine();
                if (!(inputeGender=="1"||inputeGender=="2"||inputeGender=="3"))
                    continue;// 把一二传进去      
                //  再根据性别来更新数据库
                string updataGender = $"update UserT1 set Gender='{inputeGender}'where UsersName='{userTM.UsersName}'";//   更新单一属性下对应主键的数据数据
                int count = SQLHelper.EditData(updataGender);
                if (count>0)
                {
                    Console.WriteLine($"修改{userTM.UsersName}的性别成功");
                    break;
                }
                else
                {
                    Console.WriteLine("修改失败,请重新输入");
                    continue;
                }
            }
        }
    }
}
