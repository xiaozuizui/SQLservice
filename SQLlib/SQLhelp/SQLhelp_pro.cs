using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLlib.BASE;
using MySql.Data.MySqlClient;

namespace SQLlib.SQLhelp
{
    class SQLhelp_pro : SQLhelp
    {
        private Project project;

        public SQLhelp_pro(Project p)
        {
            project = p;
            project.publishTime = DateTime.Now.ToString();
            INSERT_Str = "INSERT INTO `studentdata`.`project` (`title`, `content`, `publisherId`, `publishTime`, `tips`) VALUES (" + "'"
                + project.title + "', '"
                + project.content + "', '"
                + project.publisherId + "', '"
                + project.publishTime + "', '"
                + Convert.ToInt16(project.tips).ToString() + "');";

        }

        public void PublishProject(out int projectId,out RETUEN ret)
        {
            projectId = 0;
            try
            {
                
                MySqlCommand publishProcmd = new MySqlCommand(INSERT_Str, SQL_Connection);
                publishProcmd.ExecuteNonQuery();

                string queryID = "SELECT pid FROM project WHERE publishTime='" + project.publishTime.ToString() + "'";
                projectId = Convert.ToUInt16(new MySqlCommand(queryID, SQL_Connection).ExecuteScalar());
                project.projectId = projectId;
                string ExecuteHead = "UPDATE `studentdata`.`tips` SET `projectId`='";
                string Executestr = null;

                foreach (LABEL l in Enum.GetValues(typeof(LABEL)))
                {
                    if (project.Judge(project.tips & l))
                    {
                        string queryTipsProIds = "SELECT projectId FROM tips WHERE content='" + l.ToString() + "'";

                        MySqlCommand quercmd = new MySqlCommand(queryTipsProIds, SQL_Connection);
                        //  MySqlDataReader rdr;
                        string projectIds = quercmd.ExecuteScalar().ToString();
                        // rdr = quercmd.ExecuteReader();
                        if (projectIds != null)
                        {
                            Tips tip = new Tips();
                            tip.proID = projectIds;
                            tip.AddproId(project.projectId.ToString());
                            Executestr += ExecuteHead + tip.proID + "' WHERE `content`='" + l.ToString() + "';";
                        }
                        else
                        {
                            Executestr += ExecuteHead + project.projectId.ToString() + '*' + "' WHERE `content`='" + l.ToString() + "';";
                        }
                    }
                }
                MySqlCommand procmd = new MySqlCommand(Executestr, SQL_Connection);
                procmd.ExecuteNonQuery();
                ret= RETUEN.PublishProject_Succeed;
            }
            catch
            {
                ret = RETUEN.PublishProject_Failed;
            }
        }

        public void GetProjectContent(out string c, out RETUEN ret)
        {
            string query_content_Str = "SELECT content FROM project WHERE pid = " + project.projectId.ToString();
            MySqlCommand query_content_cmd = new MySqlCommand(query_content_Str, SQL_Connection);
            try
            {
                string content_Str = query_content_cmd.ExecuteScalar().ToString();
                c = content_Str;
                ret = RETUEN.GetProjectContent_Succeed;
            }
           
           catch
            {
                c = null;
                ret = RETUEN.GetProjectContent_Failed;
            }
        }
    }
}
