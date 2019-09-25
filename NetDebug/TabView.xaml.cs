using Dragablz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// TabView.xaml 的交互逻辑
    /// </summary>
    public partial class TabView : UserControl
    {
        public TabView()
        {
            InitializeComponent();
            DataContext = new TabViewModel();
        }
    }

    public class TabViewModel
    {
        public ObservableCollection<HeaderedItemViewModel> Items { get; } = new ObservableCollection<HeaderedItemViewModel>();
        public IInterTabClient InterTabClient { get; } = new TabViewInterTabClient();
    }

    public class TabViewInterTabClient : IInterTabClient
    {
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            var view = new MainWindow();
            var model = new TabViewModel();
            view.DataContext = model;
            return new NewTabHost<Window>(view, view.InitialTabablzControl.InitialTabablzControl);
        }

        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }
    }
}
