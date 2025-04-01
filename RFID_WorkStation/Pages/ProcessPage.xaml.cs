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

        private byte[] StringToByteArray(string hex)
        {
            int length = hex.Length / 2;
            byte[] bytes = new byte[length];

            for (int i = 0; i < length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        private void ReadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TagRead.readTag())
            {
                // 清空 ComboBox
                UIDBox.Items.Clear();

                // 遍历 uidList，将 byte[] 转换为可读的字符串并添加到 ComboBox
                foreach (var uid in TagRead.uidList)
                {
                    string uidString = BitConverter.ToString(uid).Replace("-", ""); // 转换为无分隔符的16进制字符串
                    UIDBox.Items.Add(uidString);
                }

                if (UIDBox.Items.Count > 0)
                {
                    UIDBox.SelectedIndex = 0;
                }
            }
            else {
                // 清空 ComboBox
                UIDBox.Items.Clear();
            }
        }

        private void Handle_Click(object sender, RoutedEventArgs e)
        {
            // 获取输入数据
            string barcode = BarcodeBox.Text;
            string selectedUIDString = UIDBox.SelectedItem?.ToString();

            // 确保 BarcodeBox 和 UIDBox 不能为空
            if (string.IsNullOrWhiteSpace(barcode) || string.IsNullOrWhiteSpace(selectedUIDString))
            {
                MessageBox.Show("请确保条形码和 UID 都已填写！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 转换 UID 为 byte[]
            byte[] selectedUID = StringToByteArray(selectedUIDString);

            // 执行处理操作
            bool success = false;
            try
            {
                if (TagRead.handleTag(selectedUID, barcode))
                    success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加工失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // 更新 DataGrid 显示加工结果
            UpdateProcessDataGrid(barcode, selectedUIDString, success);
        }

        private void UpdateProcessDataGrid(string barcode, string uid, bool success)
        {
            var item = new
            {
                Index = ProcessDataGrid.Items.Count + 1,  // 序号自增
                Barcode = barcode,
                UID = uid,
                Status = success ? "成功" : "失败"
            };

            ProcessDataGrid.Items.Add(item);
        }

    }
}
