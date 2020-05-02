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
    public partial class NetMonitor : UserControl
    {
        private List<PerformanceCounter> downloadCounters;
        private List<PerformanceCounter> uploadCounters;
        private DispatcherTimer dispatcherTimer;

        public NetMonitor()
        {
            InitializeComponent();
            InitPerformance();
            InitTimer();
        }

        private void InitPerformance()
        {
            downloadCounters = new List<PerformanceCounter>();
            uploadCounters = new List<PerformanceCounter>();
            PerformanceCounterCategory[] pcc = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory p in pcc)
            {
                if (p.CategoryName == "Network Interface")
                {
                    foreach (string instanceName in p.GetInstanceNames())
                    {
                        if (CheckInterfaceName(instanceName))
                        {
                            PerformanceCounter[] counters = p.GetCounters(instanceName);
                            foreach (PerformanceCounter pc in counters)
                            {
                                if (pc.CounterName == "Bytes Received/sec")
                                {
                                    downloadCounters.Add(pc);
                                }
                                else if (pc.CounterName == "Bytes Sent/sec")
                                {
                                    uploadCounters.Add(pc);
                                }
                            }
                        }
                    }
                }
            }
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
            double downB = 0;
            foreach (var c in downloadCounters)
            {
                downB += c.NextValue();
            }
            double upB = 0;
            foreach (var c in uploadCounters)
            {
                upB += c.NextValue();
            }
            string uploadMB = string.Format("{0:F1} KB/s", upB / 1024);
            string downloadMB = string.Format("{0:F1} KB/s", downB / 1024);
            downloadText.Text = downloadMB;
            uploadText.Text = uploadMB;
        }

        private bool CheckInterfaceName(string name)
        {
            return !name.StartsWith("isatap");
        }
    }
}
