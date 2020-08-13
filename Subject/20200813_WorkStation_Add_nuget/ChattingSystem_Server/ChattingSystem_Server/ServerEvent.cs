using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

using log4net;
using log4net.Config;

namespace ChattingSystem_Server
{
    public class ServerEvent
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ServerEvent));

        #region METHOD AREA ***************************************************

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string LocalIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    LocalIP = ip.ToString();
                    break;
                }
            }
            return LocalIP;
        }

        public void SendServerLog(string sendData)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log_ChattingSystem.xml"));
            logger.Info("Send: " + sendData);
        }



        #endregion METHOD AREA ***************************************************
    }
}
