using RFID_WorkStation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RFID_WorkStation.Pages
{
    /// <summary>
    /// ConnectPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConnectPage : Page
    {
        public ConnectPage()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (TagRead.connectByUsb())
            {
                MessageBox.Show("连接成功");
            }
            else {
                MessageBox.Show("连接失败");
            }          
        }     
    }
}
