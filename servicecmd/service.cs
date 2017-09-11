using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SQLlib.BASE;
using SQLlib.SQLExecute;
using Newtonsoft.Json;

namespace servicecmd
{
    class service
    {
        private static byte[] result = new byte[1024];
        const string host = "10.141.131.84";
        static void Main(string[] args)
        {

            int port = 49320;
            Socket serviceSocket;
            Socket client;

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            serviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //ji
            serviceSocket.Bind(ipe);
            serviceSocket.Listen(10);



            while (true)
            {

                try
                {

                    client = serviceSocket.Accept();
                    //client = e.AcceptSocket;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("与客户建立连接");
                    Console.WriteLine(DateTime.Now);
                    byte[] recvBytes = new byte[1024];
                    int length = client.Receive(recvBytes);
                    string clientStr = Encoding.UTF8.GetString(recvBytes, 0, length);

                    //信息接受
                    System.Console.WriteLine(clientStr);

                    SQLExecute sql = new SQLExecute(clientStr);
                    sql.ExecuteEx();

                    byte[] reb;
                    reb = sql.read();

                    string s = Encoding.UTF8.GetString(reb);
                    Console.WriteLine(s);
                    Console.WriteLine(sql.ret);
                    client.Send(reb);
                    client.Close();
                }
                catch
                {
                    Console.WriteLine("未知错误");
                    
                }
            }
        }
    }
}
