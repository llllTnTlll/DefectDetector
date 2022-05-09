using DefectDetector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DefectDetector.Helper
{
    public class SocketHelper
    {
        private readonly string _IpAddress;
        private readonly int _port;

        public ResultPkg Result { get; set; }

        public SocketHelper(string ip_address, int port)
        {
            // 初始化端口设置
            this._IpAddress = ip_address;
            this._port = port;
        }
        public Socket Connect()
        {
            // 创建与python服务的链接
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_IpAddress), _port));
            }
            catch (Exception ex)
            {
                clientSocket.Close();
                throw new Exception("套接字连接异常", ex);
            }
            return clientSocket;
        }

        public ResultPkg RecvPkg(Socket clientSocket, CommandPkg cmdPkg)
        {
            // 发送命令包
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(cmdPkg, options);
            byte[] command = Encoding.UTF8.GetBytes(jsonString);

            // 先发送数据包长度
            clientSocket.Send(Encoding.UTF8.GetBytes(command.Length.ToString().PadRight(8)));
            // 再发送数据包本体
            clientSocket.Send(command);

            // 接受运行结果
            byte[] byte_length = GetPack(clientSocket, 8);
            int recv_length = int.Parse(Encoding.UTF8.GetString(byte_length));
            byte[] pkg = GetPack(clientSocket, recv_length);
            string recv_string = Encoding.UTF8.GetString(pkg);
            Result = JsonSerializer.Deserialize<ResultPkg>(recv_string);
            return Result;

        }

        private byte[] GetPack(Socket sock, int count)
        {
            byte[] pack = new byte[count];
            int offset = 0;
            while (count > 0)
            {
                int recved = sock.Receive(pack, offset, count, SocketFlags.None);
                offset += recved;
                count -= recved;
            }
            return pack;

        }


    }
}
