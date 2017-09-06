using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLlib.BASE;
using SQLlib.SQLExecute;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace serviceFram
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static byte[] result = new byte[1024];
        int port;
        const string host = "10.141.131.84";
        private Socket serviceSocket;
        private Socket client;
       // private Socket teach;
        Thread thread;

        public MainWindow()
        {
         
            InitializeComponent();
            port = Convert.ToUInt16(Port_text.Text);
         //   thread = new Thread(new ParameterizedThreadStart(mainfun(recive,send,statues)));
        }

        private  void start_btn_Click(object sender, RoutedEventArgs e)
        {
            thread.Start();
        }

        private void mainfun(Label recive,Label send,TextBlock statues )
        {
           
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            serviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //ji
            serviceSocket.Bind(ipe);
            serviceSocket.Listen(10);

            statues.Text = "等待客户端连接";

            while (true)
            {

                try
                {
                   
                    client =  serviceSocket.Accept();
                    //client = e.AcceptSocket;
                  //  Console.WriteLine("与客户建立连接");

                    byte[] recvBytes = new byte[1024];
                    int length = client.Receive(recvBytes);
                    string clientStr = Encoding.UTF8.GetString(recvBytes, 0, length);

                    //信息接受
                    recive.Content = clientStr;
                    SQLExecute sql = new SQLExecute(JsonConvert.SerializeObject(clientStr));
                    sql.ExecuteEx();

                    byte[] reb;
                    reb = sql.read();
                    statues.Text = sql.ret.ToString();
                    string s = Encoding.UTF8.GetString(reb);
                    send.Content = s;

                    client.Send(reb);
                    client.Close();

                    

                }
                catch
                {
                   // statues.Text = "未知错误";
                }
            }
        }


        private void mainThread()
        {

        }
    }
}
