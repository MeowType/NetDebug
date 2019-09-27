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

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Tab), new PropertyMetadata(true));

        public bool IsHover
        {
            get { return (bool)GetValue(IsHoverProperty); }
            set { SetValue(IsHoverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHoverProperty =
            DependencyProperty.Register("IsHover", typeof(bool), typeof(Tab), new PropertyMetadata(false));

        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Tab), new PropertyMetadata("No Title"));

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            OnClose?.Invoke(sender, e);
        }
    }
}
