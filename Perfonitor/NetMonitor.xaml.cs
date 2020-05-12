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

        public NetMonitor()
        {
            InitializeComponent();
            InitPerformance();
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

        public void Update()
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
            string uploadKB = Format(upB);
            string downloadKB = Format(downB);
            downloadText.Text = downloadKB;
            uploadText.Text = uploadKB;
        }

        private bool CheckInterfaceName(string name)
        {
            return !name.StartsWith("isatap");
        }

        private string Format(double value)
        {
            if (value < 1024)
            {
                return string.Format("{0:F0} B/s", value);
            }
            if (value < 1024 * 1024)
            {
                return string.Format("{0:F1} KB/s", value / 1024);
            }
            if (value < 1024 * 1024 * 1024)
            {
                return string.Format("{0:F1} MB/s", value / 1024 / 1024);
            }
            return string.Format("{0:F1} GB/s", value / 1024 / 1024 / 1024);
        }
    }
}
