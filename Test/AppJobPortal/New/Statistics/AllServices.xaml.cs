using AppJobPortal.TcpOfferReference;
using JobPortal.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;


namespace AppJobPortal
{
    /// <summary>
    /// Interaction logic for AllServices.xaml
    /// </summary>
    public partial class AllServices : UserControl
    {

        private IOfferService _offerproxy;

        public AllServices()
        {
            InitializeComponent();
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");
            var offers = _offerproxy.GetAllOffers();
            int home = offers.Where(x => x.Category.ToString() == Category.Home.ToString() && x.RatePerHour < 200).Count();
            int home2 = offers.Where(x => x.Category.ToString() == Category.Home.ToString() && x.RatePerHour > 200).Count();
            int it = offers.Where(x => x.Category.ToString() == Category.IT.ToString() && x.RatePerHour < 200).Count();
            int it2 = offers.Where(x => x.Category.ToString() == Category.IT.ToString() && x.RatePerHour > 200).Count();
            int tutoring = offers.Where(x => x.Category.ToString() == Category.Tutoring.ToString() && x.RatePerHour < 200).Count();
            int tutoring2 = offers.Where(x => x.Category.ToString() == Category.Tutoring.ToString() && x.RatePerHour > 200).Count();
            int media = offers.Where(x => x.Category.ToString() == Category.Media.ToString() && x.RatePerHour < 200).Count();
            int media2 = offers.Where(x => x.Category.ToString() == Category.Media.ToString() && x.RatePerHour > 200).Count();
            int arch = offers.Where(x => x.Category.ToString() == Category.Architecture.ToString() && x.RatePerHour < 200).Count();
            int arch2 = offers.Where(x => x.Category.ToString() == Category.Architecture.ToString() && x.RatePerHour > 200).Count();
            int repairs = offers.Where(x => x.Category.ToString() == Category.Repairs.ToString() && x.RatePerHour < 200).Count();
            int repairs2 = offers.Where(x => x.Category.ToString() == Category.Repairs.ToString() && x.RatePerHour > 200).Count();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Less than 200kr/h",
                    Values = new ChartValues<int> { home, it, tutoring,media,arch,repairs }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "More than 200kr/h",
                Values = new ChartValues<int> { home2, it2, tutoring2, media2, arch2, repairs2 }
            });

            //also adding values updates and animates the chart automatically

            Labels = new[] { "Home", "IT", "Tutoring", "Media", "Architecture", "Repairs" };
            Formatter = value => ((int)value).ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}