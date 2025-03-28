using RFID_WorkStation.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RFID_WorkStation
{
    public partial class MainWindow : Window
    {
        private List<MenuItem> menuItems;
        private TextWriter _originalConsoleOut;
        private ConsoleWriter _consoleWriter;

        public MainWindow()
        {
            InitializeComponent();
            InitializeConsoleOutput();  // 先初始化控制台输出
            InitializeNavigation();   // 然后初始化导航
        }

        private void InitializeConsoleOutput()
        {
            _originalConsoleOut = Console.Out;
            _consoleWriter = new ConsoleWriter(ConsoleOutput);
            Console.SetOut(_consoleWriter);

            // 测试输出
            Console.WriteLine("系统初始化完成...");
            Console.WriteLine("控制台输出已重定向到界面");
        }

        private void InitializeNavigation()
        {
            menuItems = new List<MenuItem>
            {
                new MenuItem { Title = "连接", Icon = "/Images/connect.png", PageType = typeof(Pages.ConnectPage), IsEnabled = true },
                new MenuItem { Title = "配置", Icon = "/Images/config.png", PageType = typeof(Pages.ConfigPage), IsEnabled = true },
                new MenuItem { Title = "加工", Icon = "/Images/process.png", PageType = typeof(Pages.ProcessPage), IsEnabled = true }
            };

            NavigationMenu.ItemsSource = menuItems;
            NavigationMenu.SelectedIndex = 0;
        }

        private void OnConfigReload(object sender, EventArgs e)
        {
            EnableAllMenuItems();
        }

        public void EnableAllMenuItems()
        {
            foreach (var menuItem in menuItems)
            {
                menuItem.IsEnabled = true;
            }

            NavigationMenu.ItemsSource = null;
            NavigationMenu.ItemsSource = menuItems;
            NavigationMenu.SelectedIndex = 0;
        }

        private void NavigationMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = NavigationMenu.SelectedItem as MenuItem;
            if (selectedItem?.PageType != null && selectedItem.IsEnabled)
            {
                ContentFrame.Navigate(Activator.CreateInstance(selectedItem.PageType));              
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            TagRead.disConnect();

            Console.SetOut(_originalConsoleOut);
            _consoleWriter?.Dispose();
            base.OnClosed(e);
        }
    }

    public class ConsoleWriter : TextWriter
    {
        private readonly TextBox _textBox;
        private bool _disposed;

        public ConsoleWriter(TextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        public override void Write(char value)
        {
            if (_disposed) return;

            _textBox.Dispatcher.Invoke(() =>
            {
                _textBox.AppendText(value.ToString());
                _textBox.ScrollToEnd();
            });
        }

        public override void Write(string value)
        {
            if (_disposed) return;

            _textBox.Dispatcher.Invoke(() =>
            {
                _textBox.AppendText(value);
                _textBox.ScrollToEnd();
            });
        }

        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _textBox.Dispatcher.Invoke(() => _textBox.Clear());
            }

            _disposed = true;
            base.Dispose(disposing);
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type PageType { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}