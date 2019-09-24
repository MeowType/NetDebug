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
    using static Utils;

    /// <summary>
    /// TargetPort.xaml 的交互逻辑
    /// </summary>
    public partial class TargetPort : UserControl
    {
        public TargetPort()
        {
            InitializeComponent();
        }

        public string DefaultPort
        {
            get => (string)GetValue(DefaultPortProperty);
            set => SetValue(DefaultPortProperty, value);
        }

        public ushort? Port
        {
            get
            {
                try { return Convert.ToUInt16(Target_port.Text); }
                catch
                {
                    Target_port.Text = "Not A Port";
                    return null;
                }
            }
            set => Target_port.Text = value.HasValue ? $"{value}" : "random";
        }

        private void Target_port_Loaded(object sender, RoutedEventArgs e)
        {
            Target_port.Text = DefaultPort;
        }
    }
}
