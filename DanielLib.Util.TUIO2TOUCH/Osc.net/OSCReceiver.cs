using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace DanielLib.Util.TUIO2TOUCH.Osc.net
{
    public class OSCReceiver
    {
        protected UdpClient udpClient;
        protected int localPort;

        public OSCReceiver(int localPort)
        {
            this.localPort = localPort;
            Connect();
        }

        public void Connect()
        {
            if (this.udpClient != null) Close();
            this.udpClient = new UdpClient(this.localPort);
        }

        public void Close()
        {
            if (this.udpClient != null) this.udpClient.Close();
            this.udpClient = null;
        }

        public OSCPacket Receive()
        {
            try
            {
                IPEndPoint ip = null;
                byte[] bytes = this.udpClient.Receive(ref ip);
                if (bytes != null && bytes.Length > 0)
                    return OSCPacket.Unpack(bytes);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return null;
        }
    }
}
