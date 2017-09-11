using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLlib.BASE;
using SQLlib.SQLhelp;
using Newtonsoft.Json;


namespace SQLlib.SQLExecute
{
     public class SQLExecute
    {
        public string jsonStr;
        public Operations operation;
        public object ob;
        public RETUEN ret;
      public  SQLExecute(string s)
        {
            jsonStr = s;
        }

        public void ExecuteEx()
        {
            ob = new object();
            ret = RETUEN.No_ERRO;
            operation = JsonConvert.DeserializeObject<Operations>(jsonStr);

            if(operation.Judge(OPERATION.SQL_STU&operation.Operation))//1
            {
                SQLExecute_stu Execute_stu = new SQLExecute_stu(jsonStr,operation);
                Execute_stu.Execute(out ob,out ret);
            }
            else if(operation.Judge(OPERATION.SQL_PRO & operation.Operation))//2
            {
                SQLExecute_pro Execut_pro = new SQLExecute_pro(jsonStr,operation);
                Execut_pro.Execute(out ob, out ret);
            }
        }
        
        public byte [] read()
        {
            byte[] returnbyte=null;
            returnbyte = new byte[1];
            if (operation.Judge(OPERATION.UpdateOwnerInfo&operation.Operation))//1
            {
                return Encoding.UTF8.GetBytes("1");
                //returnbyte[0] = 1;
                //Console.WriteLine(ret);
            }
            else if(operation.Judge(OPERATION.GetGuestInfo&operation.Operation))//2
            {
                Student stu = ob as Student;
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(stu));
            }
            else if(operation.Judge(OPERATION.GetProjectCards&operation.Operation))//3
            {
                List<Project> returnpro = ob as List<Project>;
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(returnpro));

            }
            else if(operation.Judge(OPERATION.GetGuestInfoByTip&operation.Operation))//4
            {
                List<string> returnstu = ob as List<string>;
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(returnstu));
            }


            else if(operation.Judge(OPERATION.PublishProject&operation.Operation))//5
            {
                int projectId = Convert.ToUInt16(ob);
                return Encoding.UTF8.GetBytes(projectId.ToString());

            }
            else if(operation.Judge(OPERATION.GetProjectContent&operation.Operation))//6
            {
                string content = ob as string;
                if (content == null)
                    returnbyte[0] = 0;
                else
                    return Encoding.UTF8.GetBytes(content);
            }
            return returnbyte; 
        }      
    }
}
