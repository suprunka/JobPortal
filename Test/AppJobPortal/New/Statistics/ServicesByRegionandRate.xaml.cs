using AppJobPortal.TcpOfferReference;
using AppJobPortal.TcpUserReference;
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
    /// Interaction logic for ServicesByRegionandRate.xaml
    /// </summary>
    public partial class ServicesByRegionandRate : UserControl
    {
        private IOfferService _offerproxy;
        private IUserService _userproxy;
        public ServicesByRegionandRate()
        {
            InitializeComponent();
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");
            _userproxy = new UserServiceClient("UserServiceTcpEndpoint");
            var offers = _offerproxy.GetAllOffers();
            var users = _userproxy.GetAll();


            int hovedstadenAvg = 0;
            int midtyllandAvg = 0;
            int nordjyllandAvg = 0;
            int sjallandAvg = 0;
            int syddanmarkAvg = 0;
            int hovedstadenAvgLower = 0;
            int midtyllandAvgLower = 0;
            int nordjyllandAvgLower = 0;
            int sjallandAvgLower = 0;
            int syddanmarkAvgLower = 0;
            foreach (var offer in offers)
            {
                var userRegion = users.Where(x => x.LoggingId == offer.AuthorId).First().Region;
                double avg = _offerproxy.GetAvgOfServiceRates(offer.Id);
                switch (userRegion)
                {
                    case Region.Hovedstaden:
                        if (avg < 3)
                        {
                            hovedstadenAvgLower++;
                        }
                        else
                        {
                            hovedstadenAvg++;
                        }
                       
                        break;
                    case Region.Midtjylland:
                        if (avg < 3)
                        {
                            midtyllandAvgLower++;
                        }
                        else
                        {
                            midtyllandAvg++;
                        }

                     
                        break;
                    case Region.Nordjylland:
                        if (avg < 3)
                        {
                            nordjyllandAvgLower++;
                        }
                        else
                        {
                            nordjyllandAvg++;
                        }

                       
                        break;
                    case Region.Sjalland:
                        if (avg < 3)
                        {
                            sjallandAvgLower++;
                        }
                        else
                        {
                            sjallandAvg++;
                        }

                        break;
                    case Region.Syddanmark:
                        if (avg < 3)
                        {
                            syddanmarkAvgLower++;
                        }
                        else
                        {
                            syddanmarkAvg++;
                        }

                        break;

                }
               
            }
            
           
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Lower than 3 stars",
                    Values = new ChartValues<int> { hovedstadenAvgLower, midtyllandAvgLower, nordjyllandAvgLower, sjallandAvgLower, syddanmarkAvgLower }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Higher than 3 stars",
                Values = new ChartValues<int> { hovedstadenAvg, midtyllandAvg, nordjyllandAvg, sjallandAvg, syddanmarkAvg }
            });

            //also adding values updates and animates the chart automatically


                Labels = new[] { "Hovedstaden", "Midtjylland", "Nordjylland", "Sjalland", "Syddanmark" };
            Formatter = value => ((int)value).ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}