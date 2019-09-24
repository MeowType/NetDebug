using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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

namespace MeowType.NetDebug
{
    /// <summary>
    /// Udp.xaml 的交互逻辑
    /// </summary>
    public partial class Udp : UserControl
    {
        public Udp()
        {
            InitializeComponent();
        }

        void SetEnables()
        {
            if (socket != null)
            {
                UDP_button.Content = "Close";

                UDP_Local_ip.IsEnabled = false;
                UDP_Local_port.IsEnabled = false;

                UDP_type_defalut.IsEnabled = false;
                UDP_type_broadcast.IsEnabled = false;
                UDP_type_multicast.IsEnabled = false;
                UDP_multicast_ip.IsEnabled = false;

                MsgBox.Send__Button.IsEnabled = true;
            }
            else
            {
                UDP_button.Content = "Open";

                UDP_Local_ip.IsEnabled = true;
                UDP_Local_port.IsEnabled = true;

                UDP_type_defalut.IsEnabled = true;
                UDP_type_broadcast.IsEnabled = true;
                UDP_type_multicast.IsEnabled = true;
                UDP_multicast_ip.IsEnabled = true;

                MsgBox.Send__Button.IsEnabled = false;
            }
        }

        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            if (socket != null) ToClose();
            else ToOpen();
        }

        private void MsgBox_OnSend(object sender, RoutedEventArgs e, Func<string> Send_Msg)
        {
            ToSend(Send_Msg);
        }

        private void UDP_type_broadcast_Checked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.EnableBroadcast();
        }

        private void UDP_type_broadcast_UnChecked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.DisableBroadcast();
        }

        private void UDP_type_multicast_Checked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.IsEnabled = false;
            UDP_Local_ip.IsEnabled = false;
        }

        private void UDP_type_multicast_Unchecked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.IsEnabled = true;
            UDP_Local_ip.IsEnabled = true;
        }
    }
}
