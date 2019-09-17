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
    /// LocalPort.xaml 的交互逻辑
    /// </summary>
    public partial class LocalPort : UserControl
    {
        public LocalPort()
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
                if (Local_port.Text.ToLower() != "random")
                {
                    try { return Convert.ToUInt16(Local_port.Text); }
                    catch
                    {
                        Local_port.Text = "Not A Port";
                        return null;
                    }
                }
                else { return 0; }
            }
            set => Local_port.Text = value.HasValue? $"{value}" : "random";
        }

        private void Local_port_Loaded(object sender, RoutedEventArgs e)
        {
            Local_port.Text = DefaultPort;
        }
    }
}
