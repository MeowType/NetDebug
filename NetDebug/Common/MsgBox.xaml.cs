using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// MsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBox : UserControl
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        public delegate void OnSendHandler(object sender, RoutedEventArgs e, Func<string> Send_Msg);
        public event OnSendHandler OnSend;

        public Button Send_Button => Send__Button;

        public string Send_Msg()
        {
            return new TextRange(Send__Msg.Document.ContentStart, Send__Msg.Document.ContentEnd).Text;
        }

        private void MsgBox_Loaded(object sender, RoutedEventArgs e)
        {
            Msg_Box.Document.Blocks.Clear();
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Msg_Box.Document.Blocks.Clear();
        }

        private void Clear_Send_Button_Click(object sender, RoutedEventArgs e)
        {
            Send__Msg.Document.Blocks.Clear();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            OnSend?.Invoke(sender, e, Send_Msg);
        }

        private void Send_Msg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Send__Button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
