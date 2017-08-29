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
     class SQLExecute_stu
    {
        private SQLhelp_stu stu_help;
        private Operations operation;
        private string jsonStr;
        public SQLExecute_stu(string s,Operations op)
        {
            operation = op;
            jsonStr = s;
            stu_help = new SQLhelp_stu(JsonConvert.DeserializeObject<Student>(jsonStr));
            stu_help.Initialization();
        }
        
        public void Execute(out object ob,out RETUEN ret)
        {
            
            ret = RETUEN.No_ERRO;
            ob = null;

            if (operation.Judge(OPERATION.UpdateOwnerInfo&operation.Operation))
            {
                ret = stu_help.UpdateOwnerInfo();
               
            }
           else if (operation.Judge(OPERATION.GetGuestInfo&operation.Operation))
            {
                Student stu = new Student();
                stu_help.GetGuestInfo( out stu,out  ret);
                ob = stu as object;
            }
            else if(operation.Judge(OPERATION.GetProjectCards&operation.Operation))
            {
                List<Project> list_pro = new List<Project>();
                stu_help.GetProjectCards(out list_pro,out ret);
                ob = list_pro as object;
            }
            else if(operation.Judge(OPERATION.GetGuestInfoByTip&operation.Operation))
            {
                stu_help.GetGuestInfoByTip();
                
            }

          

        }
    }
}
