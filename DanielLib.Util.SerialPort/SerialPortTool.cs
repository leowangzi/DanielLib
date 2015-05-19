using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Util.SerialPort
{
    public class SerialPortTool : IDisposable
    {
        public string SerialPortNumber { get;private set; }
        public int BaudRateNumber { get; private set; }
        public bool IsOpened { get; private set; }
        private System.IO.Ports.SerialPort sP = null;

        public SerialPortTool()
        {
            this.IsOpened = false;
        }

        public SerialPortTool(string comPort, int baudRate) :this()
        {
            this.SerialPortNumber = comPort;
            this.BaudRateNumber = baudRate;
        }

        public void Init()
        {
            try
            {
                sP = new System.IO.Ports.SerialPort(SerialPortNumber, BaudRateNumber);
                sP.Open();
                this.IsOpened = true;
            }
            catch
            {
                this.IsOpened = false;
            }
        }

        public void WriteLine(string text)
        {
            if (this.IsOpened == true)
            {
                try
                {
                    sP.WriteLine(text);
                }
                catch
                {
                	
                }
                
            }
        }

        public void Dispose()
        {
           if (sP != null)
           {
               sP.Close();
               sP.Dispose();
               sP = null;
           }
        }
    }
}
