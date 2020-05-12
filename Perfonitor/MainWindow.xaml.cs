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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Perfonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private NotifyIcon notifyIcon;
        private DispatcherTimer timer;
        private System.Windows.Forms.MenuItem cpu, ram, disk, net;

        public MainWindow()
        {
            InitializeComponent();

            MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) =>
            {
                DragMove();
            });

            InitNotifyIcon();
            InitTimer();
        }

        private void InitNotifyIcon()
        {
            ShowInTaskbar = false;

            cpu = new System.Windows.Forms.MenuItem("CPU") { Checked = true };
            ram = new System.Windows.Forms.MenuItem("RAM") { Checked = true };
            disk = new System.Windows.Forms.MenuItem("Disk") { Checked = true };
            net = new System.Windows.Forms.MenuItem("Ethernet") { Checked = true };
            System.Windows.Forms.MenuItem linechart = new System.Windows.Forms.MenuItem("Show Line Chart") { Checked = true };
            System.Windows.Forms.MenuItem show = new System.Windows.Forms.MenuItem("Show") { Checked = true, Enabled = false };
            System.Windows.Forms.MenuItem hide = new System.Windows.Forms.MenuItem("Hide");
            System.Windows.Forms.MenuItem topMost = new System.Windows.Forms.MenuItem("Top Most") { Checked = true };
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");

            cpu.Click += new EventHandler((sender, e) =>
            {
                if (cpu.Checked)
                {
                    cpu.Checked = false;
                    processorMonitor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cpu.Checked = true;
                    processorMonitor.Visibility = Visibility.Visible;
                }
            });
            ram.Click += new EventHandler((sender, e) =>
            {
                if (ram.Checked)
                {
                    ram.Checked = false;
                    memoryMonitor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ram.Checked = true;
                    memoryMonitor.Visibility = Visibility.Visible;
                }
            });
            disk.Click += new EventHandler((sender, e) =>
            {
                if (disk.Checked)
                {
                    disk.Checked = false;
                    diskMonitor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    disk.Checked = true;
                    diskMonitor.Visibility = Visibility.Visible;
                }
            });
            net.Click += new EventHandler((sender, e) =>
            {
                if (net.Checked)
                {
                    net.Checked = false;
                    netMonitor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    net.Checked = true;
                    netMonitor.Visibility = Visibility.Visible;
                }
            });
            linechart.Click += new EventHandler((sender, e) =>
            {
                if (linechart.Checked)
                {
                    linechart.Checked = false;
                    UnshowLineChart();
                }
                else
                {
                    linechart.Checked = true;
                    ShowLineChart();
                }
            });
            show.Click += new EventHandler((sender, e) =>
            {
                ShowWindow(show, hide);
            });
            hide.Click += new EventHandler((sender, e) =>
            {
                HideWindow(show, hide);
            });
            topMost.Click += new EventHandler((sender, e) =>
            {
                SwitchTopMost(topMost);
            });
            exit.Click += new EventHandler((sender, e) =>
            {
                ExitWindow();
            });
            notifyIcon = new NotifyIcon
            {
                Text = "Perfonitor",
                Visible = true,
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] 
                    { cpu, ram, disk, net, linechart, show, hide, topMost, exit })
            };
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ShowWindow(show, hide);
                }
            });
        }

        private void InitTimer()
        {
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = true
            };
            timer.Tick += new EventHandler((sender, e) =>
            {
                Update();
            });
            timer.Start();
        }

        private void Update()
        {
            if (cpu.Checked)
            {
                processorMonitor.Update();
            }
            if (ram.Checked)
            {
                memoryMonitor.Update();
            }
            if (disk.Checked)
            {
                diskMonitor.Update();
            }
            if (net.Checked)
            {
                netMonitor.Update();
            }
        }

        private void ShowLineChart()
        {
            processorMonitor.ShowLineChart();
            memoryMonitor.ShowLineChart();
        }

        private void UnshowLineChart()
        {
            processorMonitor.UnshowLineChart();
            memoryMonitor.UnshowLineChart();
        }

        private void ShowWindow(System.Windows.Forms.MenuItem show, System.Windows.Forms.MenuItem hide)
        {
            Visibility = System.Windows.Visibility.Visible;
            Activate();
            show.Checked = true;
            show.Enabled = false;
            hide.Checked = false;
            hide.Enabled = true;
        }

        private void HideWindow(System.Windows.Forms.MenuItem show, System.Windows.Forms.MenuItem hide)
        {
            Visibility = System.Windows.Visibility.Hidden;
            show.Checked = false;
            show.Enabled = true;
            hide.Checked = true;
            hide.Enabled = false;
        }

        private void SwitchTopMost(System.Windows.Forms.MenuItem topMost)
        {
            if (topMost.Checked)
            {
                topMost.Checked = false;
                Topmost = false;
            }
            else
            {
                topMost.Checked = true;
                Topmost = true;
            }
        }

        private void ExitWindow()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
