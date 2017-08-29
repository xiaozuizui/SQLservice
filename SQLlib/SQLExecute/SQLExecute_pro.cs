using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLlib.BASE;
using SQLlib.SQLhelp;
namespace SQLlib.SQLExecute
{
    class SQLExecute_pro
    {
        private SQLhelp_pro pro_help;
        private Operations operation;
        private string jsonStr;
        public SQLExecute_pro(string s,Operations op)
        {
            operation = op;
            jsonStr = s;
            pro_help = new SQLhelp_pro(JsonConvert.DeserializeObject<Project>(jsonStr));
            pro_help.Initialization();
        }

        public void Execute(out object content,out RETUEN ret)
        {
            ret = RETUEN.No_ERRO;
            content = new object();
            if(operation.Judge(OPERATION.PublishProject&operation.Operation))
            {
                int proid;
                pro_help.PublishProject(out proid, out ret);
                content = proid as object;
            }
            else if(operation.Judge(OPERATION.GetProjectContent&operation.Operation))
            {
                string s;
                pro_help.GetProjectContent(out s, out ret);
                content = s as object;
            }
        }
    }
}
