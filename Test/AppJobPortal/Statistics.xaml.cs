using System.Windows.Controls;
using System;
using LiveCharts;
using LiveCharts.Wpf;
using AppJobPortal.TcpOfferReference;
using System.Collections.Generic;
using AppJobPortal.Models;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppJobPortal

{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        private IOfferService _offerproxy;

        public Statistics()
        {

            InitializeComponent();
            //_offerproxy = new OfferServiceClient()
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42,10 }
            });

            //also adding values updates and animates the chart automatically

            Labels = new[] { "Home", "IT", "Tutoring", "Media" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}