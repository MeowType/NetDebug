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
    /// TabBar.xaml 的交互逻辑
    /// </summary>
    public partial class TabBar : UserControl
    {
        public TabBar()
        {
            InitializeComponent();
        }

        public bool IsFocus
        {
            get { return (bool)GetValue(IsFocusProperty); }
            set {
                SetValue(IsFocusProperty, value);
                add_but.IsFocus = value;
            }
        }

        // Using a DependencyProperty as the backing store for IsFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFocusProperty =
            DependencyProperty.Register("IsFocus", typeof(bool), typeof(TabBar), new PropertyMetadata(true));

    }
}
