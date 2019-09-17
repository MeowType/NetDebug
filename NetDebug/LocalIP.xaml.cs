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
    /// LocalIP.xaml 的交互逻辑
    /// </summary>
    public partial class LocalIP : UserControl
    {
        public LocalIP()
        {
            InitializeComponent();
        }

        public IPAddress Ip => Local_ip.Text.ParseIp();

        private void Local_ip_Loaded(object sender, RoutedEventArgs e)
        {
            Local_ip.Items.Clear();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Select(ip => ip.ToString()).Distinct())
            {
                Local_ip.Items.Add(ip);
            }
            Local_ip.Items.Insert(0, Local_ip_localhost);
            Local_ip.SelectedIndex = 0;
        }
    }
}
