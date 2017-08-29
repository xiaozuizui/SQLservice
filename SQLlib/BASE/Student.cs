//#define TESTADD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLlib.BASE
{

   public class Student : Operations
    {

        public string name { get; set; }
        public string stuId { get; set; }
        public string password { get; set; }
        public string grade { get; set; }
        public string major { get; set; }
        public string qqNum { get; set; }
        public string telNum { get; set; }
        public string collections { get; set; }



        public List<int> GetCollections()
        {
            List<int> Collections = new List<int>();
            int index_start = 0;

            int index_end;
            do
            {
                index_end = collections.IndexOf('*', index_start);
                if (index_end < 0)
                    break;

                string mids = new string(collections.ToCharArray(), index_start, index_end - index_start);
                Collections.Add(Convert.ToInt16(mids));
                index_start = index_end + 1;
            }
            while (index_end > 0);

            return Collections;

        }

        public RETUEN AddCollection(int projectId)
        {

#if TESTADD
            List<int> Collections = GetCollections();
            foreach (int collection in Collections)
            {
                if (projectId == collection)
                    return RETUEN.Add_Stu_Collection_Failed;
            }
 #endif

            collections += projectId.ToString() + "*";
            return RETUEN.Add_Stu_Collection_Succeed;
        }


        public RETUEN DeletCollection(int projectId)
        {
            List<int> Collections = GetCollections();
            foreach (int collection in Collections)
            {
                if (projectId == collection)
                {
                    int index_dstart = collections.IndexOf(projectId.ToString());
                    int index_dend = collections.IndexOf('*', index_dstart);

                    string collection1 = new string(collections.ToCharArray(), 0, index_dstart);
                    string collection2 = new string(collections.ToCharArray(), index_dend + 1, collections.Length - index_dend - 1);

                    collections = collection1 + collection2;

                    return RETUEN.Delete_Stu_Collection_Succeed;
                }
            }
            return RETUEN.Delete_Stu_Collection_Failed;
        }

    }
}
