using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Perfonitor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            new Mutex(true, "Perfonitor", out bool createNew);
            if (!createNew || Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("程序已运行，请在托盘区寻找Perfonitor图标");
                Current.Shutdown();
            }
            else
            {
                base.OnStartup(e);
            }
        }
    }
}
