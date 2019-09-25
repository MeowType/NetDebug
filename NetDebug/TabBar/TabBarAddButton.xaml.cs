using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// TabBarAddButton.xaml 的交互逻辑
    /// </summary>
    public partial class TabBarAddButton : Button
    {
        public TabBarAddButton()
        {
            InitializeComponent();
        }

        public bool IsFocus
        {
            get { return (bool)GetValue(IsFocusProperty); }
            set {
                SetValue(IsFocusProperty, value);
                label.Foreground = value ? new SolidColorBrush(new Color() { R = 252, G = 252, B = 252, A = 255 }) : new SolidColorBrush(new Color() { R = 64, G = 64, B = 64, A = 255 });
            }
        }

        // Using a DependencyProperty as the backing store for IsFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFocusProperty =
            DependencyProperty.Register("IsFocus", typeof(bool), typeof(TabBarAddButton), new PropertyMetadata(true));
    }
}
