using AppJobPortal.TcpOfferReference;
using AppJobPortal.TcpOrderReference;
using AppJobPortal.TcpUserReference;
using JobPortal.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Controls;


namespace AppJobPortal
{
    /// <summary>
    /// Interaction logic for UserBoughtServices.xaml
    /// </summary>
    public partial class UserBoughtServices : UserControl
    {
        private IUserService _userproxy;
        private IOfferService _offerproxy;
        private IOrderService _orderproxy;
        public UserBoughtServices()
        {
            InitializeComponent();

            _userproxy = new UserServiceClient("UserServiceTcpEndpoint");
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");
            _orderproxy = new OrderServiceClient("OrderServiceTcpEndpoint");
            int hovedstadenBought = 0;
            int midtyllandBought = 0;
            int nordjyllandBought = 0;
            int sjallandBought = 0;
            int syddanmarkBought = 0;

            int hovedstadenSold = 0;
            int midtyllandSold = 0;
            int nordjyllandSold = 0;
            int sjallandSold = 0;
            int syddanmarkSold = 0;

            var users = _userproxy.GetAll();
            foreach (var user in users)
            {
                int numberOfBought = _offerproxy.GetAllBought(user.LoggingId).Count();
                int numberOfSold = _orderproxy.GetAllSalelines().Where(x => x.AuthorId == user.LoggingId).Count();

                switch (user.Region)
                {
                    case Region.Hovedstaden:
                        hovedstadenBought += numberOfBought;
                        hovedstadenSold += numberOfSold;
                        break;
                    case Region.Midtjylland:
                        midtyllandBought += numberOfBought;
                        midtyllandSold += numberOfSold;
                        break;
                    case Region.Nordjylland:
                        nordjyllandBought += numberOfBought;
                        nordjyllandSold += numberOfSold;
                        break;
                    case Region.Sjalland:
                        sjallandBought += numberOfBought;
                        sjallandSold += numberOfSold;
                        break;
                    case Region.Syddanmark:
                        syddanmarkBought += numberOfBought;
                        syddanmarkSold += numberOfSold;
                        break;

                }

            }
            //

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Bought",
                    Values = new ChartValues<int> { hovedstadenBought, midtyllandBought, nordjyllandBought, sjallandBought, syddanmarkBought }
                }
            };
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Sold",
                Values = new ChartValues<int> { hovedstadenSold, midtyllandSold, nordjyllandSold, sjallandSold, syddanmarkSold }
            });




            //also adding values updates and animates the chart automatically

            Labels = new[] { "Hovedstaden", "Midtjylland", "Nordjylland", "Sjalland", "Syddanmark" };
            Formatter = value => ((int) value).ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}