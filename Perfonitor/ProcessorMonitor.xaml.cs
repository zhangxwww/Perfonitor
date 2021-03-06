﻿using Microsoft.Research.DynamicDataDisplay;
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
        private int currentSecond = TIMESPAN;

        private const int TIMESPAN = 10;

        public ProcessorMonitor()
        {
            InitializeComponent();
            InitPerformance();
            InitDataSource();
            InitPlotter();
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
            plotter.AddLineGraph(dataSource, Color.FromRgb(0x28, 0x2c, 0x37), 1);
            plotter.Viewport.FitToView();
            plotter.Children.Remove(plotter.MouseNavigation);
            plotter.Children.Remove(plotter.KeyboardNavigation);
            plotter.Children.Remove(plotter.Legend);
            plotter.Children.Remove(plotter.DefaultContextMenu);
            plotter.AxisGrid.Remove();
        }

      
        public void Update()
        {
            double percent = performanceCounter.NextValue();
            if (plotter.Visibility == Visibility.Visible)
            {
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
            }
            string processorUsage = string.Format("{0:F0}%", percent);
            usageText.Text = processorUsage;
        }

        public void ShowLineChart()
        {
            plotter.Visibility = Visibility.Visible;
        }

        public void UnshowLineChart()
        {
            plotter.Visibility = Visibility.Collapsed;
        }
    }
}
