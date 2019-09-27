using Dragablz;
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

namespace MeowType.NetDebug
{
    /// <summary>
    /// EmptyTab.xaml 的交互逻辑
    /// </summary>
    public partial class EmptyTab : UserControl
    {
        public EmptyTab()
        {
            InitializeComponent();
        }

        public HeaderedItemViewModel TabView
        {
            get { return (HeaderedItemViewModel)GetValue(TabViewProperty); }
            set { SetValue(TabViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TabView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabViewProperty =
            DependencyProperty.Register("TabView", typeof(HeaderedItemViewModel), typeof(EmptyTab), new PropertyMetadata(default(HeaderedItemViewModel)));

        private void Udp_Click(object sender, RoutedEventArgs e)
        {
            TabView.Header = "Udp";
            TabView.Content = new Udp();
        }

        private void TcpServer_Click(object sender, RoutedEventArgs e)
        {
            TabView.Header = "Tcp Server";
            TabView.Content = new TcpServer();
        }
    }
}
