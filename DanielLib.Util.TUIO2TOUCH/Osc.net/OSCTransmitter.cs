using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace DanielLib.Util.TUIO2TOUCH.Osc.net
{
    public class OSCTransmitter
    {
        protected UdpClient udpClient;
        protected string remoteHost;
        protected int remotePort;

        public OSCTransmitter(string remoteHost, int remotePort)
        {
            this.remoteHost = remoteHost;
            this.remotePort = remotePort;
            Connect();
        }

        public void Connect()
        {
            if (this.udpClient != null) Close();
            this.udpClient = new UdpClient(this.remoteHost, this.remotePort);
        }

        public void Close()
        {
            this.udpClient.Close();
            this.udpClient = null;
        }

        public int Send(OSCPacket packet)
        {
            int byteNum = 0;
            byte[] data = packet.BinaryData;
            try
            {
                byteNum = this.udpClient.Send(data, data.Length);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }

            return byteNum;
        }

    }
}
