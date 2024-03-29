﻿using System;
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
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool IsFocus
        {
            get { return (bool)GetValue(IsFocusProperty); }
            private set {
                SetValue(IsFocusProperty, value);
                //tab_bar.IsFocus = value;
            }
        }

        // Using a DependencyProperty as the backing store for IsFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFocusProperty =
            DependencyProperty.Register("IsFocus", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));

        private void Window_Activated(object sender, EventArgs e)
        {
            IsFocus = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            IsFocus = false;
        }
    }
}
