using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowType.NetDebug
{
    public class MainObj
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new App();
            app.InitializeComponent();

            var mw = new MainWindow();
            ((TabViewModel)mw.InitialTabablzControl.DataContext).Items.Add(NewTab.New());
            mw.Show();

            app.Run();
        }
    }
}
