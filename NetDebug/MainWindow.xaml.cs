using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void SetEnables()
        {
            if (socket != null)
            {
                Tab_Tcp_Client.IsEnabled = false;
                Tab_Tcp_Server.IsEnabled = false;
                Tab_Udp.IsEnabled = false;
                Tab_Custom.IsEnabled = false;

                UDP_button.Content = "Close";

                UDP_Local_ip.IsEnabled = false;
                UDP_Local_port.IsEnabled = false;

                UDP_type_defalut.IsEnabled = false;
                UDP_type_broadcast.IsEnabled = false;
                UDP_type_multicast.IsEnabled = false;

                Send_Button.IsEnabled = true;
            }
            else
            {
                Tab_Tcp_Client.IsEnabled = true;
                Tab_Tcp_Server.IsEnabled = true;
                Tab_Udp.IsEnabled = true;
                Tab_Custom.IsEnabled = true;

                UDP_button.Content = "Open";

                UDP_Local_ip.IsEnabled = true;
                UDP_Local_port.IsEnabled = true;

                UDP_type_defalut.IsEnabled = true;
                UDP_type_broadcast.IsEnabled = true;
                UDP_type_multicast.IsEnabled = true;

                Send_Button.IsEnabled = false;
            }
        }

        IPAddress ParseIp(string str)
        {
            if (!IPAddress.TryParse(str, out var ip))
            {
                switch (str)
                {
                    case "localhost": return IPAddress.Loopback;
                    case "broadcast": return IPAddress.Broadcast;
                    default: return IPAddress.Any;
                }
            }
            return ip;
        }

        void Log(EndPoint ep, string msg, bool from = false)
        {
            msg = msg.Replace("\0", string.Empty);
            string addr;
            if (ep is IPEndPoint iep)
            {
                addr = $"{iep.Address} :{iep.Port}";
            }
            else
            {
                addr = ep.ToString();
            }

            Dispatcher.Invoke(() =>
            {
                MsgBox.Document.Blocks.Add(new Paragraph()
                {
                    Inlines = {
                        new Run($"[{DateTime.Now.ToString("zzz yyyy.MM.dd tt hh:mm:ss:fff")} ]"){
                            Foreground = new SolidColorBrush(Colors.DarkGray),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new Run(from ? " <== " : " ==> ") {
                            Foreground = new SolidColorBrush(from ? Colors.Green : Colors.Blue),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new Run($"{{ {addr} }}"){
                            Foreground = new SolidColorBrush(Colors.DarkGray),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new LineBreak(),
                        new Run(msg){
                            Foreground = new SolidColorBrush(from ? Colors.Green : Colors.Blue),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                    },
                    Typography =
                    {
                        StandardLigatures = true,
                        ContextualLigatures = true,
                        DiscretionaryLigatures = true,
                        HistoricalLigatures = true,
                        ContextualAlternates = true,
                    },
                }) ;
            });

        }

        enum NowType
        {
            TcpServer, TcpClient, Udp
        }
        NowType nowType = NowType.Udp;

        Socket socket;
        CancellationTokenSource loop;
        List<Task> loops = new List<Task>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (socket != null)
            {
                socket.Close();
                socket = null;
                SetEnables();
                loop.Cancel();
                var nloop = loop;
                loop = null;
                Task.Run(async () =>
                {
                    try
                    {
                        await Task.WhenAll(loops);
                    }
                    finally
                    {
                        nloop.Dispose();
                    }
                });
                return;
            }
            var ip = ParseIp(UDP_Local_ip.Text);
            int port;
            try { port = Convert.ToInt32(UDP_Local_port.Text); }
            catch
            {
                UDP_Local_port.Text = "Not A Port";
                return;
            }
            if(port > 65535)
            {
                UDP_Local_port.Text = "Port too large";
                return;
            }

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(new IPEndPoint(ip, port));

                SetEnables();
                nowType = NowType.Udp;

                socket.EnableBroadcast = UDP_type_broadcast.IsChecked ?? false;

                loop = new CancellationTokenSource();
                loops.Add(Task.Run(() =>
                {
                    while (socket != null)
                    {
                        var buffer = new byte[1024];
                        
                        try
                        {
                            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                            socket.ReceiveFrom(buffer, SocketFlags.None, ref ep);

                            var str = Encoding.Default.GetString(buffer);

                            Log(ep, str, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }, loop.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            if(nowType == NowType.Udp)
            {
                var ip = ParseIp(UDP_Target_ip.Text);
                int port;
                try { port = Convert.ToInt32(UDP_Target_port.Text); }
                catch
                {
                    UDP_Local_port.Text = "Not A Port";
                    return;
                }
                if (port > 65535)
                {
                    UDP_Local_port.Text = "Port too large";
                    return;
                }
                try
                {
                    var @byte = Encoding.Default.GetBytes(new TextRange(Send_Msg.Document.ContentStart, Send_Msg.Document.ContentEnd).Text);
                    if(UDP_type_broadcast.IsChecked ?? false)
                    {
                        socket.SendTo(@byte, @byte.Length, SocketFlags.Broadcast, new IPEndPoint(ip, port));
                    }
                    else
                    {
                        socket.SendTo(@byte, @byte.Length, SocketFlags.None, new IPEndPoint(ip, port));
                    }
                    Log(new IPEndPoint(ip, port), new TextRange(Send_Msg.Document.ContentStart, Send_Msg.Document.ContentEnd).Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void UDP_type_broadcast_Checked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.Items.Add(UDP_Target_ip_broadcast);
        }

        private void UDP_type_broadcast_UnChecked(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.Items.Remove(UDP_Target_ip_broadcast);
        }

        private void UDP_Target_ip_Loaded(object sender, RoutedEventArgs e)
        {
            UDP_Target_ip.Items.Remove(UDP_Target_ip_broadcast);
        }

        private void Send_Msg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Send_Button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
