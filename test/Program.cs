using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLlib.BASE;
using SQLlib.SQLExecute;
using Newtonsoft.Json;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            Student teststu = new Student();
           // teststu.stuId = "201585082";
            teststu.Operation = OPERATION.GetGuestInfoByTip;
            teststu.grade = "2015";
            teststu.name = "嘴嘴";
            teststu.qqNum = "785897146";
            teststu.telNum = "18388481204";
            teststu.tips = (LABEL)1;
            teststu.major = "计算机";
            

            Project proj = new Project();
           //   proj.content = "嘴嘴的项目3";
           //    proj.publisherId = "201585081";
         //      proj.title = "嘴嘴的title3";
          //  proj.tips = LABEL.CPP | LABEL.CSHARP | LABEL.JAVA;
              proj.projectId = 23;
            proj.Operation = OPERATION.GetProjectCards;
            

            SQLExecute sql = new SQLExecute(JsonConvert.SerializeObject(teststu));
            sql.ExecuteEx();

            byte[] reb;
            reb = sql.read();
            Console.WriteLine(sql.ret);
            string s = Encoding.UTF8.GetString(reb);
        }
    }
}
