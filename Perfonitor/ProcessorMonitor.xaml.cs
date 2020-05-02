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
    public partial class ProcessorMonitor : UserControl
    {
        private ObservableDataSource<Point> dataSource;
        private PerformanceCounter performanceCounter;
        private DispatcherTimer dispatcherTimer;
        private int currentSecond = TIMESPAN;

        private const int TIMESPAN = 10;

        public ProcessorMonitor()
        {
            InitializeComponent();
            InitPerformance();
            InitDataSource();
            InitPlotter();
            InitTimer();
        }

        private void InitPerformance()
        {
            performanceCounter = new PerformanceCounter()
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
        }

        private void InitDataSource()
        {
            dataSource = new ObservableDataSource<Point>();
            for (int _ = 0; _ < TIMESPAN; ++_)
            {
                dataSource.AppendAsync(base.Dispatcher, new Point(0, 0));
            }
        }

        private void InitPlotter()
        {
            plotter.AddLineGraph(dataSource, Colors.Green, 1);
            plotter.Viewport.FitToView();
            plotter.Children.Remove(plotter.MouseNavigation);
            plotter.Children.Remove(plotter.KeyboardNavigation);
            plotter.Children.Remove(plotter.Legend);
            plotter.AxisGrid.Remove();
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
            double percent = performanceCounter.NextValue();
            Point p = new Point()
            {
                X = currentSecond,
                Y = percent
            };
            dataSource.AppendAsync(base.Dispatcher, p);
            double xaxis = currentSecond > 10 ? currentSecond - 10
                                              : 0;
            ++currentSecond;
            plotter.Viewport.Visible = new System.Windows.Rect(xaxis, 0, 10, 100);
            string processorUsage = string.Format("{0:F0}%", percent);
            usageText.Text = processorUsage;
        }
    }
}
