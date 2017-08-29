using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLlib.BASE
{
   public class Project:Operations
    {
        public int projectId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string publisherId { get; set; }
        public string publishTime { get; set; }

        // public OPERATION Operation { get; set; }
    }
}
