using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeowType.NetDebug
{
    public class PortKind : UserControl { }

    internal static class Utils
    {
        public readonly static DependencyProperty DefaultPortProperty = DependencyProperty.Register("DefaultPort", typeof(string), typeof(PortKind), new PropertyMetadata("12345"));

        public readonly static FontFamily FiraCodeLight = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Fira Code Light");

        public static IPAddress ParseIp(this string str)
        {
            if (!IPAddress.TryParse(str, out var ip))
            {
                switch (str.ToLower())
                {
                    case "loopback":
                    case "localhost": return IPAddress.Loopback;
                    case "broadcast": return IPAddress.Broadcast;

                    case "ipv6any":
                    case "v6any":
                    case "anyv6":
                    case "anyipv6": return IPAddress.IPv6Any;

                    case "ipv4any":
                    case "v4any":
                    case "anyv4":
                    case "anyipv4":
                    case "any":
                    default: return IPAddress.Any;
                }
            }
            return ip;
        }
    }
}
