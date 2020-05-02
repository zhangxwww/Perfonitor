using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Threading;

namespace Perfonitor
{
    /// <summary>
    /// ProcessorMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class DiskMonitor : UserControl
    {
        private PerformanceCounter readCounter;
        private PerformanceCounter writeCounter;
        private DispatcherTimer dispatcherTimer;

        public DiskMonitor()
        {
            InitializeComponent();
            InitPerformance();
            InitTimer();
        }

        private void InitPerformance()
        {
            readCounter = new PerformanceCounter()
            {
                CategoryName = "PhysicalDisk",
                CounterName = "Disk Read Bytes/sec",
                InstanceName = "_Total"
            };
            writeCounter = new PerformanceCounter()
            {
                CategoryName = "PhysicalDisk",
                CounterName = "Disk Write Bytes/sec",
                InstanceName = "_Total"
            };
        }

        private void InitTimer()
        {
            dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = true
            };
            dispatcherTimer.Tick += new EventHandler((sender, e) =>
            {
                Update();
            });
            dispatcherTimer.Start();
        }

        private void Update()
        {
            double readB = readCounter.NextValue();
            double writeB = writeCounter.NextValue();
            string diskWrite = string.Format("{0:F1} MB/s", writeB / 1024 / 1024);
            string diskRead = string.Format("{0:F1} MB/s", readB / 1024 / 1024);
            writeText.Text = diskWrite;
            readText.Text = diskRead;
        }
    }
}
