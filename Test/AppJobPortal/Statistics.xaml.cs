using System.Windows.Controls;
using System;
using System.Windows.Controls;
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
       
public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        private readonly IOfferService _proxy;
        private SeriesCollection SeriesCollection;

        public Statistics()
        {

            InitializeComponent();
            _proxy = new OfferServiceClient("OfferServiceTcpEndpoint");

            var home = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.Home).Count();//.Where(x => x.RatePerHour > 200).Count();
            var architecture = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.Architecture).Count(); //.Where(x => x.RatePerHour > 200).Count();
            var it = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.IT).Count();//.Where(x => x.RatePerHour > 200).Count();
            var media = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.Media).Where(x => x.RatePerHour > 200).Count();
            var repairs = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.Repairs).Where(x => x.RatePerHour > 200).Count();
            var tutoring = _proxy.GetAllOffers().Where(x => x.Category == JobPortal.Model.Category.Tutoring).Where(x => x.RatePerHour > 200).Count();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Below 200 kr/h",
                    Values = new ChartValues<double> { home, architecture, it }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Abowe 200 kr/h",
                Values = new ChartValues<double> { 11, 26, 32, 22, 2, 21 }
            });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Home", "Architecture", "IT" };

            DataContext = this;
        }
       
}
    }
