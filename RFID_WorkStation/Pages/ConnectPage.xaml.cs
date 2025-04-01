using RFID_WorkStation.Common;
using System.Windows;
using System.Windows.Controls;

namespace RFID_WorkStation.Pages
{
    public partial class ConnectPage : Page
    {
        // 连接状态
        private static bool _isConnected = false;

        private MainWindow _mainWindow;

        public ConnectPage()
        {
            InitializeComponent();

            _mainWindow = (MainWindow)Application.Current.MainWindow;

            InitializeUIState();
          
        }

        private void InitializeUIState()
        {
            // 根据保存的状态初始化UI
            OpenButton.IsEnabled = !_isConnected;
            CloseButton.IsEnabled = _isConnected;
            CommunicationInterface.IsEnabled = !_isConnected;
            PidSelection.IsEnabled = !_isConnected;

            // 更新主窗口菜单状态
            _mainWindow.UpdateMenuItemsEnabledState(_isConnected);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (TagRead.connectByUsb())
            {
                _isConnected = true;
                OpenButton.IsEnabled = false;
                CloseButton.IsEnabled = true;
                CommunicationInterface.IsEnabled = false;
                PidSelection.IsEnabled = false;

                // 更新主窗口菜单状态
                _mainWindow.UpdateMenuItemsEnabledState(true);
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (TagRead.disConnect())
            {
                _isConnected = false;
                OpenButton.IsEnabled = true;
                CloseButton.IsEnabled = false;
                CommunicationInterface.IsEnabled = true;
                PidSelection.IsEnabled = true;

                // 更新主窗口菜单状态
                _mainWindow.UpdateMenuItemsEnabledState(false);
                MessageBox.Show("断开成功");
            }
            else
            {
                MessageBox.Show("断开失败");
            }
        }

        private void CommunicationInterface_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 通信接口选择变化时的处理
        }

        private void PidSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // PID选择变化时的处理
        }
    }
}