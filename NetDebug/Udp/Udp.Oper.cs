using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
    public partial class Udp : UserControl
    {
        UdpClient socket;
        List<UdpClient> sendClients = new List<UdpClient>();
        CancellationTokenSource loop;
        List<Task> loops = new List<Task>();

        void ToClose()
        {
            socket.Close();
            socket.Dispose();
            if (sendClients != null)
            {
                sendClients.ForEach(send =>
                {
                    send.Close();
                    send.Dispose();
                });
                sendClients = null;
            }
            socket = null;
            SetEnables();
            try
            {
                loop.Cancel();
                var nloop = loop;
                var nloops = loops;
                loop = null;
                loops = new List<Task>();
                Task.Run(async () =>
                {
                    try
                    {
                        await Task.WhenAll(nloops);
                    }
                    finally
                    {
                        nloop.Dispose();
                    }
                });
            }
            catch (Exception ex)
            {
                MsgBox.LogError(ex);
            }
            return;
        }

        void ToOpen()
        {
            var ip = UDP_type_multicast.IsChecked ?? false ? IPAddress.Any : UDP_Local_ip.Ip;
            var mport = UDP_Local_port.Port;
            if (!mport.HasValue) return;
            int port = mport.Value;

            try
            {
                var uc = new UdpClient(new IPEndPoint(ip, port));
                socket = uc;
                port = ((IPEndPoint)uc.Client.LocalEndPoint).Port;

                MsgBox.LogSystem($"Open on {{ {ip} :{port} }}");

                SetEnables();

                uc.EnableBroadcast = UDP_type_broadcast.IsChecked ?? false;

                if (UDP_type_broadcast.IsChecked ?? false)
                {
                    MsgBox.LogSystem($"Enabled Broadcast on {{ {UDP_Target_ip.Ip} }}");
                }

                if (UDP_type_multicast.IsChecked ?? false)
                {
                    var mip = UDP_multicast_ip.Text.ParseIp();

                    sendClients = new List<UdpClient>();

                    var msgs = (from networkInterface in NetworkInterface.GetAllNetworkInterfaces()
                                where networkInterface.OperationalStatus == OperationalStatus.Up
                                select networkInterface.GetIPProperties()
                                    .UnicastAddresses.First(addr =>
                                        addr.Address.AddressFamily == AddressFamily.InterNetwork)?.Address)
                    .Where(address => address != null)
                    .Select(addr =>
                    {
                        uc.JoinMulticastGroup(mip, addr);
                        sendClients.Add(new UdpClient(new IPEndPoint(addr, port)));
                        return $"Join Multicast Group {{ {mip} on {addr} }}";
                    });

                    MsgBox.LogSystem(msgs.ToArray());

                    //uc.JoinMulticastGroup(mip);
                    //LogSystem($"Join Multicast Group {{ {mip} }}");
                }

                loop = new CancellationTokenSource();
                loops.Add(Task.Run(() =>
                {
                    while (socket != null || socket != uc)
                    {
                        try
                        {
                            var ep = new IPEndPoint(IPAddress.Any, 0);

                            var buffer = uc.Receive(ref ep);

                            var str = Encoding.Default.GetString(buffer);

                            MsgBox.Log(ep, str, true);
                        }
                        catch (Exception ex)
                        {
                            if (socket == null || socket != uc)
                            {
                                MsgBox.LogSystem("Closed");
                            }
                            else
                            {
                                MsgBox.LogError(ex);
                            }
                            return;
                        }
                    }
                }, loop.Token));
            }
            catch (Exception ex)
            {
                MsgBox.LogError(ex);
                return;
            }
        }
		
		void ToSend(Func<string> Send_Msg)
        {
            var ip = UDP_type_multicast.IsChecked ?? false ? UDP_multicast_ip.Text.ParseIp() : UDP_Target_ip.Ip;
            var mport = UDP_Target_port.Port;
            if (!mport.HasValue) return;
            int port = mport.Value;
            try
            {
                var @byte = Encoding.Default.GetBytes(Send_Msg());
                if (UDP_type_multicast.IsChecked ?? false)
                {
                    sendClients.ForEach(send =>
                    {
                        send.Send(@byte, @byte.Length, new IPEndPoint(ip, port));
                    });
                }
                else if (socket is UdpClient uc)
                {
                    uc.Send(@byte, @byte.Length, new IPEndPoint(ip, port));
                }
                else
                {
                    throw new SocketException();
                }
                MsgBox.Log(new IPEndPoint(ip, port), Send_Msg());
            }
            catch (Exception ex)
            {
                MsgBox.LogError(ex);
                return;
            }
        }
    }
}
