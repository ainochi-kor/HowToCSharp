using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChattingSystem_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerForm());

            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));

        }
    }
}
