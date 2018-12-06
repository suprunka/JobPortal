using AppJobPortal.TcpOfferReference;
using JobPortal.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
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
            _offerproxy = new OfferServiceClient();
            int home = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Home.ToString() && x.RatePerHour < 200).Count();
            int home2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Home.ToString() && x.RatePerHour > 200).Count();
            int it = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.IT.ToString() && x.RatePerHour < 200).Count();
            int it2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.IT.ToString() && x.RatePerHour > 200).Count();
            int tutoring = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Tutoring.ToString() && x.RatePerHour < 200).Count();
            int tutoring2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Tutoring.ToString() && x.RatePerHour > 200).Count();
            int media = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Media.ToString() && x.RatePerHour < 200).Count();
            int media2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Media.ToString() && x.RatePerHour > 200).Count();
            int arch = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Architecture.ToString() && x.RatePerHour < 200).Count();
            int arch2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Architecture.ToString() && x.RatePerHour > 200).Count();
            int repairs = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Repairs.ToString() && x.RatePerHour < 200).Count();
            int repairs2 = _offerproxy.GetAllOffers().Where(x => x.Category.ToString() == Category.Repairs.ToString() && x.RatePerHour > 200).Count();
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
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}