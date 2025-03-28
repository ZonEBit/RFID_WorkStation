using RFID_WorkStation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// ProcessPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessPage : Page
    {
        public ProcessPage()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(TagRead.readTag));         
        }
    }
}
