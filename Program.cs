using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多语言支持04
{
    //  为安全性
    //  为通用性
    //  不直接存储数据本身
    internal class Program
    {
        static void Main(string[] args)
        {

            //  语言判断
            Console.WriteLine("请选择展示性别的语言");
            Console.WriteLine("1：中文 2：English 3：繁体中文,输入错误数字默认为中文");
            string number = Console.ReadLine();
            //  用语音文档
            #region 语言文档
            if (number=="2")// 此处的1 2 是分辨语言的，在把对应语言的内容输入进去
            {
                InfoHelper.Gender1="Male";
                InfoHelper.Gender2="Female";
                InfoHelper.Gender3="unknown";
                InfoHelper.Info1="Welcome to the xxx system";
                InfoHelper.Info6="Welcome to your login, dear @NickName";// 用@来占位，后面替换

            }
            else if (number=="3")
            {
                InfoHelper.Gender1="繁体男";
                InfoHelper.Gender2="繁体女";
                InfoHelper.Gender3="繁体未知性别";
                InfoHelper.Info1="歡迎使用xxx系統";
                InfoHelper.Info6="歡迎你登錄，尊敬的@NickName";
            }
            else
            {
                InfoHelper.Gender1="男";
                InfoHelper.Gender2="女";
                InfoHelper.Gender3="未知性别";
                InfoHelper.Info1="欢迎使用xxx系统";
                InfoHelper.Info6="欢迎你登录，尊敬的@NickName";

            }
            #endregion


            //  DataTable table = null;//   老版登录界面用
            UserT1Model userTM = null;
            //  界面部分
            Console.WriteLine(InfoHelper.Info1);//  下面都可以重复如此
            while (true)
            {
                Console.Write("请输入账号名:");
                string inputeName = Console.ReadLine();
                Console.Write("请输入密码:");
                string inputePwd = Console.ReadLine();



                userTM=UserT1Operation.Login(inputeName, inputePwd);
                if (userTM!=null)
                {
                    break;
                }

                #region 老版登录界面
                //  为看的舒心用方法
                //sql = $"select*from UserT1 where UsersName='{inputeName}'and Password='{inputePwd}'";
                //table = SelectData(sql);
                //if (table.Rows.Count<=0)
                //{
                //    Console.WriteLine("账号或密码不正确");

                //}
                //else
                //{
                //    userTM=UserT1Operation.operation(table, 0);//   用方法完成下面这个
                //    //  另一个方法
                //    userTM=UserT1Operation.DataRowToUserTModel(table.Rows[0]);
                //    #region 方法实现的内容
                //    ////  怕数据库的属性写错
                //    //userTM.UsersName=table.Rows[0]["UsersName"].ToString();
                //    //userTM.Password=table.Rows[0]["Password"].ToString();
                //    //userTM.NickName=table.Rows[0]["NickName"].ToString();
                //    //userTM.Gender=table.Rows[0]["Gender"].ToString();
                //    #endregion
                //    break;
                //}
                #endregion

            }
            Console.WriteLine("登录成功");
            Console.WriteLine($"欢迎你登录，尊敬的{userTM.NickName}");//   打出昵称
                                                             //  换语言且替换      string类型下有Replace
            InfoHelper.Info6=InfoHelper.Info6.Replace("@NickName", userTM.NickName);
            Console.WriteLine(InfoHelper.Info6);



            Console.WriteLine("你可看到全部的数据，除了密码");


            //  读取性别，如果是空或许null，提醒用户输入信息
            string gender = userTM.Gender;
            //  验证用户密码和性别输入
            if (string.IsNullOrEmpty(gender))//  空或者null则返回true
            {
                Console.WriteLine($"尊敬的{userTM.NickName}你好，你的消息性别还不完整，请输入性别");

                UserT1Operation.UPDataGender(userTM);
                #region 不用方法
                //while (true)
                //{
                //    Console.WriteLine("请输入1：男 2：女 3:未知性别");
                //    string inputeGender = Console.ReadLine();
                //    if (!(inputeGender=="1"||inputeGender=="2"||inputeGender=="3"))
                //        continue;// 把一二传进去

                //    #region 老版传男女
                //    //if (inputeGender=="1")
                //    //{
                //    //    gender="男";
                //    //}
                //    //else if (inputeGender=="2")
                //    //{
                //    //    gender="女";
                //    //}
                //    //else
                //    //    continue;
                //    #endregion

                //    //  再根据性别来更新数据库
                //    string updataGender = $"update UserT1 set Gender='{inputeGender}'where UsersName='{userTM.UsersName}'";//   更新单一属性下对应主键的数据数据
                //    int count = SQLHelper.EditData(updataGender);
                //    if (count>0)
                //    {
                //        Console.WriteLine($"修改{userTM.UsersName}的性别成功");
                //        break;
                //    }
                //    else
                //    {
                //        Console.WriteLine("修改失败,请重新输入");
                //        continue;
                //    }

                //}
                #endregion

            }




            #region 展示数据库中所有的数据
            #region 老查询方法，注释
            //string sql = "select*from UserT1";
            //Console.WriteLine("用户名 昵称 性别");
            //DataTable tableTwo = SQLHelper.SelectData(sql);
            //List<UserT1Model> users = new List<UserT1Model>();
            //for (int i = 0; i<tableTwo.Rows.Count; i++)
            //{
            //    UserT1Model model = new UserT1Model();
            //    model=UserT1Operation.operation(tableTwo, i);//  用方法完成下面这个步骤
            //    //  另一个相同作用的方法方法
            //    model=UserT1Operation.DataRowToUserTModel(tableTwo.Rows[i]);
            //    #region 方法实现的内容
            //    //model.UsersName=tableTwo.Rows[i]["UsersName"].ToString();// 还可以把这一步抽象为方法
            //    //model.Password=tableTwo.Rows[i]["Password"].ToString();
            //    //model.NickName=tableTwo.Rows[i]["NickName"].ToString();
            //    //model.Gender=tableTwo.Rows[i]["Gender"].ToString();
            //    #endregion
            //    users.Add(model);
            //}
            //for (int i = 0; i<users.Count; i++)
            //{
            //    if (users[i].Gender=="1")
            //        users[i].Gender=InfoHelper.Gender1;
            //    else if (users[i].Gender=="2")
            //        users[i].Gender=InfoHelper.Gender2;
            //    else if (users[i].Gender=="3")
            //        users[i].Gender=InfoHelper.Gender3;
            //    Console.WriteLine($"{users[i].UsersName}   {users[i].NickName}   {users[i].Gender}");//    $用来字符串拼接
            //}
            #endregion


            //  查询所有数据
            List<UserT1Model> usersOne = UserT1Operation.AllUsers();

            for (int i = 0; i<usersOne.Count; i++)
            {
                if (usersOne[i].Gender=="1")
                    usersOne[i].Gender=InfoHelper.Gender1;
                else if (usersOne[i].Gender=="2")
                    usersOne[i].Gender=InfoHelper.Gender2;
                else if (usersOne[i].Gender=="3")
                    usersOne[i].Gender=InfoHelper.Gender3;

                //  查询其成绩
                List<UserScoresT1Model> usersScore = UserScoresT1Operation.GetScoreByUsersName(usersOne[i].UsersName);
                Console.WriteLine($"{usersOne[i].UsersName}用户输入成绩如下");
                Console.WriteLine("入录时间  语文成绩  数学成绩  英语成绩");
                for (int j=0; j<usersScore.Count; j++)
                {           
                   
                    Console.WriteLine($"{usersOne[i].UsersName}   {usersOne[i].NickName}   {usersOne[i].Gender}   {usersScore[j].RecordTime}   {usersScore[j].Chinese}   {usersScore[j].Math}   {usersScore[j].English}");//    $用来字符串拼接
                }
                Console.WriteLine($"{usersOne[i].UsersName}   {usersOne[i].NickName}   {usersOne[i].Gender}");

            }


            #region 老版遍历 不显示结果
            //for (int i = 0; i<tableTwo.Rows.Count; i++)
            //{
            //    string genderForSQL = tableTwo.Rows[i]["Gender"].ToString();

            //    //  调用语言文档（此处用类InfoHelper）
            //    if (genderForSQL=="1")//    此处的1 2是数据库中的1 2（分辨男女的）
            //        genderForSQL=InfoHelper.Gender1;
            //    else if (genderForSQL=="2")
            //        genderForSQL=InfoHelper.Gender2;
            //    else if (genderForSQL=="3")
            //        genderForSQL=InfoHelper.Gender3;

            #region 不用语言文档的麻烦用法
            //if (language==1)
            //{
            //    if (genderForSQL=="1")
            //        genderForSQL="男";
            //    else if (genderForSQL=="2")
            //        genderForSQL="女";
            //}

            //if (language==2)
            //{
            //    if (genderForSQL=="1")
            //        genderForSQL="Male";
            //    else if (genderForSQL=="2")
            //        genderForSQL="Famale";                   
            //}
            #endregion

            //Console.WriteLine($"{tableTwo.Rows[i]["UsersName"]}   {tableTwo.Rows[i]["NickName"]}   {genderForSQL}");//    $用来字符串拼接
            //}
            #endregion
            #endregion
            Console.ReadKey();
        }
    }
}
