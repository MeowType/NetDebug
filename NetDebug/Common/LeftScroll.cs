using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeowType.NetDebug
{
    public class LeftScroll: ContentControl
    {
        static LeftScroll()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftScroll), new FrameworkPropertyMetadata(typeof(LeftScroll)));
        }
    }
}
