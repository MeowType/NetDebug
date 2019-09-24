using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace MeowType.NetDebug
{
    /// <summary>
    /// UDPTargetIP.xaml 的交互逻辑
    /// </summary>
    public partial class UDPTargetIP : UserControl
    {
        public UDPTargetIP()
        {
            InitializeComponent();
        }

        public IPAddress Ip => UDP_Target_ip.Text.ParseIp();

        private void UDP_Target_ip_Loaded(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.Items.Remove(UDP_Target_ip_broadcast);
        }

        public void EnableBroadcast()
        {
            UDP_Target_ip.Items.Insert(0, UDP_Target_ip_broadcast);
            UDP_Target_ip.Items.Remove(UDP_Target_ip_localhost);
            UDP_Target_ip.SelectedIndex = 0;
        }

        public void DisableBroadcast()
        {
            UDP_Target_ip.Items.Remove(UDP_Target_ip_broadcast);
            UDP_Target_ip.Items.Insert(0, UDP_Target_ip_localhost);
            UDP_Target_ip.SelectedIndex = 0;
        }
    }
}
