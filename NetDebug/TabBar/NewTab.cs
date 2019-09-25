using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowType.NetDebug
{
    public static class NewTab
    {
        public static HeaderedItemViewModel New()
        {
            var item = new EmptyTab();
            var tab = new HeaderedItemViewModel()
            {
                Header = "New Tab",
                Content = item
            };
            item.TabView = tab;
            return tab;
        }
        public static Func<HeaderedItemViewModel> Factory => New;
    }
}
