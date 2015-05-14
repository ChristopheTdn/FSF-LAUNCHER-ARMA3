using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Arma3Launcher
{
    class Arma3ServerBean
    {

        private String serverName;
        private String mapName;
        private String missionName;
        private String connected;

        public void setServerName(String s)
        {
            serverName = s.Substring(5);
        }

        public void setMapName(String s)
        {
            mapName = s; //faire la correspondance
        }

        public void setMissionName(String s)
        {
            missionName = s;
        }

        public void setConnected(String s)
        {
            if (s.Length > 1)
            {
                connected = ((int)s.ToCharArray()[0]) + "/" + ((int)s.ToCharArray()[1]);
            }
            else
            {
                connected = "0/" + (int)s.ToCharArray()[0];
            }
        }

        public String getServerName()
        {
            return serverName;
        }
        public String getMapName()
        {
            return mapName;
        }
        public String getMissionName()
        {
            return missionName;
        }
        public String getConnected()
        {
            return connected;
        }
    }


    class CallArma3Server
    {

        public Arma3ServerBean call(string ip, int port)
        {
            byte[] prefix = new byte[4];
            char[] endData = new char[2];
            endData[0] = (char)0;
            endData[1] = (char)0;
            prefix[0] = unchecked((byte)(255 << 1));
            prefix[1] = unchecked((byte)(255 << 1));
            prefix[2] = unchecked((byte)(255 << 1));
            prefix[3] = unchecked((byte)(255 << 1));
            string broadcastMessage = System.Text.Encoding.UTF8.GetString(prefix) + "TSource Engine Query\0";
            var multicastAddress = IPAddress.Parse(ip);
            var signal = new ManualResetEvent(false);
            var multicastPort = port;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                var multicastEp = new IPEndPoint(multicastAddress, multicastPort);
                EndPoint localEp = new IPEndPoint(IPAddress.Any, multicastPort);
                Arma3ServerBean bean = new Arma3ServerBean();
                socket.Bind(localEp);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0); // only LAN
                var thd = new Thread(() => {
                    try
                    {
                        var response = new byte[8000];
                        socket.ReceiveFrom(response, ref localEp);
                        var str = Encoding.UTF8.GetString(response).TrimEnd(endData);
                        string[] datas = str.Split(new Char[] { '\0' });
                        bean.setServerName(datas[0]);
                        if (datas[1].Length == 0)
                            bean.setMapName("No Map");
                        else
                            bean.setMapName(datas[1]);
                        if (datas[1].Length == 0)
                            bean.setMissionName("No Mission");
                        else
                            bean.setMissionName(datas[3]);
                        if (datas[6].Length == 0)
                            bean.setConnected(datas[7]);
                        else
                            bean.setConnected(datas[6]);
                    }
                    catch
                    {
                        //Y'a plus de chaussette !
                    }
                    finally
                    {
                        signal.Set();
                    }
                });
                signal.Reset();
                thd.Start();

                socket.SendTo(Encoding.ASCII.GetBytes(broadcastMessage), 0, broadcastMessage.Length, SocketFlags.None, multicastEp);
                bool transmitOK = signal.WaitOne(1000);
                if (!transmitOK)
                {
                    bean.setServerName("#####Server Error !");
                    bean.setMapName("-");
                    bean.setMissionName("-");
                    bean.setConnected("\0");
                }
                socket.Close();
                return bean;
            }
        }
    }
}