using System.Windows.Controls;
using System;
using LiveCharts;
using LiveCharts.Wpf;

namespace AppJobPortal

{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();

                PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                DataContext = this;
            PieChart p = new PieChart();
            }

            public Func<ChartPoint, string> PointLabel { get; set; }

            private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
            {
                var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

                //clear selected slice.
                foreach (PieSeries series in chart.Series)
                    series.PushOut = 0;

                var selectedSeries = (PieSeries)chartpoint.SeriesView;
                selectedSeries.PushOut = 8;
            }
        }
    }
