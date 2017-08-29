#define LOCAL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using SQLlib.BASE;

namespace SQLlib.SQLhelp
{
    class SQLhelp
    {

#if LOCAL
        public string Connection_Str = "server=localhost;user=root;password=785897146;database=studentdata;";
#else
        public string Connection_Str = "server=123.206.14.31;user=root;password=785897146;database=studentdata;";
#endif

        public MySqlConnection SQL_Connection;
        public  string UPDATE_Str;
        public string QUERY_Str;
        public string INSERT_Str;
        public string QUERY_COUNT_Str;
        public virtual RETUEN Execute()
        {
            return RETUEN.No_ERRO;
        }
        public virtual RETUEN Initialization()
        {
            SQL_Connection = new MySqlConnection(Connection_Str);
            SQL_Connection.Open();

            return RETUEN.No_ERRO;
        }

        public void EndConnection()
        {
            SQL_Connection.Close();
        }

        
       

}
}
