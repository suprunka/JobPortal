using AppJobPortal.TcpOfferReference;
using AppJobPortal.TcpOrderReference;
using JobPortal.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for EarnedMoney.xaml
    /// </summary>
    public partial class EarnedMoney : UserControl
    {
        private IOrderService _orderproxy;
        private IOfferService _offerproxy;
        public EarnedMoney()
        {
            InitializeComponent();
            _orderproxy = new OrderServiceClient();
            _offerproxy = new OfferServiceClient();
            CultureInfo cul = CultureInfo.CurrentCulture;


            int weekNum = cul.Calendar.GetWeekOfYear(
                    DateTime.Now,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);

            var orderList = _orderproxy.GetAllOrders();
            var allSalelines = _orderproxy.GetAllSalelines();
            IDictionary<int, decimal> weeksMoney =  new Dictionary<int, decimal>();
            foreach (Order item in orderList)
            {
                if (item.OrderStatus != ""+2)
                {
                    foreach (var saleline in item.Salelines)
                    {
                        int weekN = cul.Calendar.GetWeekOfYear(
                     saleline.Date,
                     CalendarWeekRule.FirstDay,
                     DayOfWeek.Monday);
                        Order x = item;
                        if (weekN >= weekNum - 4 && weekN <= weekNum)
                        {
                            weeksMoney.Add(weekN, item.TotalPrice);
                        }
                    }
                }
                    
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,7 }
                }
            };
            CultureInfo cul = CultureInfo.CurrentCulture;


            int weekNum = cul.Calendar.GetWeekOfYear(
                    DateTime.Now,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);
            Labels = new[] { "Week" + (weekNum - 4), "Week" + (weekNum - 3), "Week" + (weekNum - 2), "Week" +(weekNum-1),"Week" + weekNum  };
            YFormatter = value => value.ToString("C");

      

            //modifying any series values will also animate and update the chart

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}