using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RFID_WorkStation
{
    public static class ProgramEntrance
    {      
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        public static void Main()
        {                       
            RFID_WorkStation.App app = new RFID_WorkStation.App();//WPF项目的Application实例，用来启动WPF项目
            app.InitializeComponent();
            app.Run();
        }
    }
}
