using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLlib.BASE
{
    public class Tips
    {
        public LABEL tip { get; set; }
        public string userID { get; set; }
        public string proID { get; set; }

        public RETUEN AddUser(string id)
        {
            userID = userID + id + '*';
            return RETUEN.Add_Tips_User_Succeed;
        }

        public RETUEN AddproId(string id)
        {
            proID = proID + id + '*';
            return RETUEN.Add_Tips_User_Succeed;
        }

        /// <summary>
        /// 获取该标签下的用户Id
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsers()
        {
            List<string> returnUser = new List<string>();

            int index_start = 0;
            int index_end;
            do
            {
                index_end = userID.IndexOf('*', index_start);
                if (index_end < 0)
                {
                    break;
                }
                string mids = new string(userID.ToCharArray(), index_start, index_end - index_start);
                returnUser.Add(mids);
                index_start = index_end + 1;
            }
            while (index_end > 0);

            return returnUser;
        }


        /// <summary>
        /// 获取该标签下的项目Id
        /// </summary>
        /// <returns></returns>
        public List<int> GetPros()
        {
            List<int> returnPros = new List<int>();

            int index_start = 0;
            int index_end;
            do
            {
                index_end = proID.IndexOf('*', index_start);
                if (index_end < 0)
                {
                    break;
                }
                string mids = new string(proID.ToCharArray(), index_start, index_end - index_start);
                returnPros.Add(Convert.ToInt16(mids));
                index_start = index_end + 1;
            }
            while (index_end > 0);

            return returnPros;
        }

        /// <summary>
        /// 删除该标签下的指定用户Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RETUEN DeletUser(string id)
        {
            List<string> uid = GetUsers();

            foreach (string ID in uid)
            {
                if (id == ID)
                {
                    int index_dstart = userID.IndexOf(ID.ToString());
                    int index_dend = userID.IndexOf('*', index_dstart);

                    string collection1 = new string(userID.ToCharArray(), 0, index_dstart);
                    string collection2 = new string(userID.ToCharArray(), index_dend + 1, userID.Length - index_dend - 1);

                    userID = collection1 + collection2;
                    return RETUEN.Delete_Tips_User_Succeed;
                }
            }
            return RETUEN.Delete_Tips_User_Failed;
        }

        public RETUEN DeletPro(int pid)
        {
            List<int> pros = GetPros();

            foreach (int pro in pros)
            {
                if (pro == pid)
                {
                    int index_dstart = proID.IndexOf(pro.ToString());
                    int index_dend = proID.IndexOf('*', index_dstart);

                    string collection1 = new string(proID.ToCharArray(), 0, index_dstart);
                    string collection2 = new string(proID.ToCharArray(), index_dend + 1, proID.Length - index_dend - 1);

                    proID = collection1 + collection2;

                    return RETUEN.Delete_Pro_Succeed;
                }

            }
            return RETUEN.Delete_Pro_Failed;
        }
    }
}
