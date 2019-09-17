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
    /// Tab.xaml 的交互逻辑
    /// </summary>
    public partial class Tab : UserControl
    {
        public Tab()
        {
            InitializeComponent();
        }

        public delegate void OnCloseClick(object sender, RoutedEventArgs e);
        public event OnCloseClick OnClose;


        public string Title
        {
            get => (string)GetValue(TabTextProperty);
            set
            {
                SetValue(TabTextProperty, value);
                TabContent.Content = value;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            OnClose?.Invoke(sender, e);
        }
    }
}
