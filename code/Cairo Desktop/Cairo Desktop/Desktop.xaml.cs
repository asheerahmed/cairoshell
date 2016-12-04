﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Interop;
using CairoDesktop.Interop;

namespace CairoDesktop
{
    /// <summary>
    /// Interaction logic for Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        public Stack<string> PathHistory = new Stack<string>();
        public DesktopIcons Icons;
        public Desktop()
        {
            InitializeComponent();

            WindowInteropHelper f = new WindowInteropHelper(this);
            int result = NativeMethods.SetShellWindow(f.Handle);
            Shell.ShowWindowBottomMost(f.Handle);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            WindowInteropHelper f = new WindowInteropHelper(this);
            int result = NativeMethods.SetShellWindow(f.Handle);
            Shell.ShowWindowBottomMost(f.Handle);

            if (Properties.Settings.Default.EnableDesktop && Icons == null)
            {
                Icons = new DesktopIcons();
                grid.Children.Add(Icons);

                if (Properties.Settings.Default.EnableDynamicDesktop)
                {
                    DesktopNavigationToolbar nav = new DesktopNavigationToolbar() { Owner = this };
                    nav.Show();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // show the windows desktop
            Interop.Shell.ToggleDesktopIcons(true);
        }
    }
}
