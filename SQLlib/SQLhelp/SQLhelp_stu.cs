using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLlib.BASE;
using MySql;
using MySql.Data.MySqlClient;

namespace SQLlib.SQLhelp
{
    class SQLhelp_stu : SQLhelp
    {
        private Student student;
        public SQLhelp_stu(Student s)
        {

            student = s;
            UPDATE_Str = "UPDATE `studentdata`.`student` SET `name`='" + student.name + "', " +
                "`grade`='" + student.grade + "', " +
                "`majoy`='" + student.major + "', " +
                "`qqnumber`='" + student.qqNum + "', " +
                "`tel`='" + student.telNum + "', " +
                "`label`='" + Convert.ToInt16(student.tips).ToString() + "' " +
                "WHERE `id`='" + student.stuId + "';";

            QUERY_Str = "SELECT id,name,grade,majoy,qqnumber,tel,label FROM student WHERE id='" + student.stuId + "'";
            QUERY_COUNT_Str = "SELECT count(*),label FROM student WHERE id='" + student.stuId + "'";

            INSERT_Str = "INSERT INTO `studentdata`.`student` (`id`, `name`, `grade`, `majoy`, `qqnumber`, `tel`, `label`) VALUES (" +
                "'" + student.stuId + "', " +
                "'" + student.name + "', '" +
                student.grade + "', '" +
                student.major + "', '" +
                student.qqNum + "', '" +
                student.telNum + "', '" +
                Convert.ToInt16(student.tips).ToString() + "');";

        }

       


        public void GetGuestInfo(out Student stu, out RETUEN ret)
        {
            MySqlCommand Query_cmd = new MySqlCommand(QUERY_Str, SQL_Connection);

            MySqlDataReader Query_DataReader = Query_cmd.ExecuteReader();

            stu = new Student();
            Query_DataReader.Read();
            if (Query_DataReader.HasRows)
            {
                stu.stuId = (string)Query_DataReader[0];
                stu.name = (string)Query_DataReader[1];
                stu.grade = (string)Query_DataReader[2];
                stu.major = (string)Query_DataReader[3];
                stu.qqNum = (string)Query_DataReader[4];
                stu.telNum = (string)Query_DataReader[5];
                stu.tips = (LABEL)Query_DataReader[6];
                ret = RETUEN.Query_HasRow;
            }
            else
            {
                ret = RETUEN.Query_NoRow;
                stu = null;
            }
            Query_DataReader.Close();
        }

        public RETUEN UpdateOwnerInfo()
        {
            bool IsUpdata;
            int Query_Count;
            int BeforLabel;

            MySqlCommand Query_cmd = new MySqlCommand(QUERY_COUNT_Str, SQL_Connection);

            //MySqlCommand sqlcmd = new MySqlCommand(_queryStr, connection);

            MySqlDataReader Query_DataReader = Query_cmd.ExecuteReader();
            Query_DataReader.Read();

            Query_Count = Convert.ToUInt16(Query_DataReader[0]);

            if (Query_Count != 0)
            {
                IsUpdata = true;
                BeforLabel = Convert.ToUInt16(Query_DataReader[1]);
            }
            else
            {
                IsUpdata = false;
                BeforLabel = 0;
            }
            Query_DataReader.Close();

            if (IsUpdata)
            {
                try
                {
                    MySqlCommand UPDATEcmd = new MySqlCommand(UPDATE_Str, SQL_Connection);
                    UPDATEcmd.ExecuteNonQuery();

                    string ExecuteHead = "UPDATE `studentdata`.`tips` SET `userId`='";

                    foreach (LABEL l in Enum.GetValues(typeof(LABEL)))
                    {
                        string Executestr = null;
                        if (student.Judge(l & student.tips) && !student.Judge(l & (LABEL)BeforLabel))
                        {

                            //add userid to tips
                            string queryuser = "SELECT userId FROM tips WHERE content='" + l.ToString() + "'";
                            MySqlCommand quercmd = new MySqlCommand(queryuser, SQL_Connection);
                            string userOfTips = quercmd.ExecuteScalar().ToString();

                            // Tips_DataReader = quercmd.ExecuteReader();
                            if (userOfTips != null)
                            {

                                Tips tip = new Tips();
                                tip.userID = userOfTips;
                                tip.AddUser(student.stuId);
                                Executestr += ExecuteHead + tip.userID + "' WHERE `content`='" + l.ToString() + "';";
                            }
                            else
                            {
                                Executestr += ExecuteHead + student.stuId + '*' + "' WHERE `content`='" + l.ToString() + "';";
                            }

                            MySqlCommand usercmd = new MySqlCommand(Executestr, SQL_Connection);
                            usercmd.ExecuteNonQuery();
                        }

                        if (!student.Judge(l & student.tips) && student.Judge(l & (LABEL)BeforLabel))
                        {
                            //delet userid to tips
                            string queryuser = "SELECT userId FROM tips WHERE content='" + l.ToString() + "'";

                            MySqlCommand quercmd = new MySqlCommand(queryuser, SQL_Connection);

                            string userOfTips = quercmd.ExecuteScalar().ToString();
                            if (userOfTips != null)
                            {
                                Tips tip = new Tips();
                                tip.userID = userOfTips;
                                tip.DeletUser(student.stuId);
                                Executestr += ExecuteHead + tip.userID + "' WHERE `content`='" + l.ToString() + "';";
                            }
                            else
                            {
                                Executestr += ExecuteHead + student.stuId + '*' + "' WHERE `content`='" + l.ToString() + "';";
                            }
                            MySqlCommand usercmd = new MySqlCommand(Executestr, SQL_Connection);
                            usercmd.ExecuteNonQuery();

                        }
                    }
                    return RETUEN.UpdateOwnerInfo_Succeed;
                }
                catch
                {
                    return RETUEN.UpdateOwnerInfo_Failed;
                }
            }
            else
            {
                try
                {
                    MySqlCommand INSERTcmd = new MySqlCommand(INSERT_Str, SQL_Connection);
                    INSERTcmd.ExecuteNonQuery();

                    if (Convert.ToUInt16(student.tips) != 0)
                    {
                        string ExecuteHead = "UPDATE `studentdata`.`tips` SET `userId`='";
                        string Executestr = null;

                        foreach (LABEL l in Enum.GetValues(typeof(LABEL)))
                        {
                            if (student.Judge(student.tips & l))
                            {
                                string queryuser = "SELECT userId FROM tips WHERE content='" + l.ToString() + "'";

                                MySqlCommand quercmd = new MySqlCommand(queryuser, SQL_Connection);
                                //  MySqlDataReader rdr;
                                string userOfTips = quercmd.ExecuteScalar().ToString();

                                if (userOfTips != null)
                                {
                                    Tips tip = new Tips();
                                    tip.userID = userOfTips;
                                    tip.AddUser(student.stuId);
                                    Executestr += ExecuteHead + tip.userID + "' WHERE `content`='" + l.ToString() + "';";

                                }
                                else
                                {
                                    Executestr += ExecuteHead + student.stuId + '*' + "' WHERE `content`='" + l.ToString() + "';";
                                }
                            }
                        }
                        MySqlCommand usercmd = new MySqlCommand(Executestr, SQL_Connection);
                        usercmd.ExecuteNonQuery();


                    }
                    return RETUEN.UpdateOwnerInfo_Succeed;
                }
                catch
                {
                    return RETUEN.UpdateOwnerInfo_Failed;
                }

            }

        }

        public void GetProjectCards(out List<Project> returnpro, out RETUEN ret)
        {
            string selectTip = "SELECT label FROM student WHERE id='" + student.stuId + "'";
            MySqlCommand tipcmd = new MySqlCommand(selectTip, SQL_Connection);
            returnpro = new List<Project>();
            List<Project> pros = new List<Project>();
            try
            {

                int tips = Convert.ToUInt16(tipcmd.ExecuteScalar());

                student.tips = (LABEL)tips;

                foreach (LABEL l in Enum.GetValues(typeof(LABEL)))
                {
                    if (student.Judge(student.tips & l))
                    {
                        string selectTips = "SELECT projectId FROM tips WHERE content='" + l.ToString() + "'";
                        MySqlCommand pidscmd = new MySqlCommand(selectTips, SQL_Connection);
                        string proid = pidscmd.ExecuteScalar().ToString();

                        Tips tip = new Tips();
                        tip.proID = proid;

                        if (tip.proID == null)
                        {
                            continue;
                        }
                        foreach (int id in tip.GetPros())
                        {
                            string quarypro = "SELECT pid,title,publisherId,publishTime,tips FROM project WHERE pid='" + id + "'";
                            MySqlCommand quaryprocmd = new MySqlCommand(quarypro, SQL_Connection);
                            MySqlDataReader quaryproread = quaryprocmd.ExecuteReader();
                            Project project = new Project();
                            quaryproread.Read();

                            project.projectId = Convert.ToUInt16(quaryproread[0]);
                            project.title = (string)quaryproread[1];
                            project.publisherId = (string)quaryproread[2];
                            project.publishTime = (string)quaryproread[3];
                            project.tips = (LABEL)quaryproread[4];

                            quaryproread.Close();
                            
                            pros.Add(project);
                        }
                    }
                }
                //returnpro.Distinct().ToList();
               returnpro = pros.Where((x, i) => pros.FindIndex(z => z.projectId == x.projectId) == i).ToList();
              //  returnpro = pros;
                ret = RETUEN.GetProjectCards_Succeed;
            }
            catch
            {
                ret = RETUEN.GetProjectCards_Failed;
            }
            //return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(returnpro));
        }


        //待定
        public void GetGuestInfoByTip()
        {
            List<Student> returnList = new List<Student>();

            string Query_str = "SELECT grade,majoy,qqnumber,tel,label FROM student WHERE label =";
        }

        List<LABEL> GetTips()
        {
            List<LABEL> returnTips = new List<LABEL>();

            foreach(LABEL l in Enum.GetValues(typeof(LABEL)))
            {
                if (student.Judge(l & student.tips))
                    returnTips.Add(l);
            }
            return returnTips;
        }
    }
}
