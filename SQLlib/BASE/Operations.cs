using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQLlib.BASE
{
    public class Operations
    {
        public OPERATION Operation { get; set; }
        public LABEL tips { get; set; }

        public bool Judge(OPERATION op)
        {
            return op > 0;
        }

        public bool Judge(LABEL la)
        {
            return la > 0;
        }
    }
}
