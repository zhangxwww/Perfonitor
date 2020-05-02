using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Perfonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryKilobytes);
        */

        public MainWindow()
        {
            InitializeComponent();

            Test();
        }

        private void Test()
        {
            /*
            GetPhysicallyInstalledSystemMemory(out long memKb);
            Debug.Print((memKb / 1024 / 1024).ToString());

            PerformanceCounter pc = new PerformanceCounter()
            {
                CategoryName = "Memory",
                CounterName = "Available MBytes"
            };
            Debug.Print(pc.NextValue().ToString());
            */
            /*
            string[] interestedItem = new string[]
            {
                //"Processor",
                "Network Interface"
                //"PhysicalDisk",
                //"Memory",
                //"FileSystem Disk Activity"
            };
            PerformanceCounterCategory[] pcc = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory p in pcc)
            {
                if (interestedItem.Contains(p.CategoryName))
                {
                    Debug.Print("**********************");
                    Debug.Print(p.CategoryName);
                    Debug.Print("----------------------");
                    string[] instance = p.GetInstanceNames();
                    foreach (string i in instance)
                    {
                        Debug.Print(i);
                        Debug.Print("+++++++++++++++");
                        PerformanceCounter[] counters = p.GetCounters(i);
                        foreach (PerformanceCounter pc in counters)
                        {
                            Debug.Print(pc.CounterName);
                        }
                    }
                }
                */
        }
    }
}
