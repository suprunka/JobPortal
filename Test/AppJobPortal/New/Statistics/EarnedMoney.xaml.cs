using AppJobPortal.TcpOfferReference;
using AppJobPortal.TcpOrderReference;
using JobPortal.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;


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
            _orderproxy = new OrderServiceClient("OrderServiceTcpEndpoint");
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");
            CultureInfo cul = CultureInfo.CurrentCulture;


            int weekNum = cul.Calendar.GetWeekOfYear(
                    DateTime.Now,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday);

            var orderList = _orderproxy.GetAllOrders();
            var allSalelines = _orderproxy.GetAllSalelines();
            IDictionary<int, double?> weeksMoney =  new Dictionary<int, double?>();
            weeksMoney.Add(weekNum - 4, 0);
            weeksMoney.Add(weekNum - 3, 0);
            weeksMoney.Add(weekNum - 2, 0);
            weeksMoney.Add(weekNum - 1, 0);
            weeksMoney.Add(weekNum , 0);

            foreach (Order item in orderList)
            {
                if (item.OrderStatus == ""+2)
                {
                    foreach (var saleline in item.Salelines)
                    {
                        int weekN = cul.Calendar.GetWeekOfYear(
                     saleline.Date,
                     CalendarWeekRule.FirstDay,
                     DayOfWeek.Monday);
                        if (weekN >= (weekNum - 4 )&& weekN <= weekNum)
                        {
                                 weeksMoney[weekN] += (double) item.TotalPrice;
                            
                            break;
                        }
                    }
                }
                    
            }
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Earned money",
                    Values = new ChartValues<double> { weeksMoney[weekNum - 4] ?? 0, weeksMoney[weekNum-3] ?? 0, weeksMoney[weekNum-2] ?? 0, weeksMoney[weekNum - 1] ?? 0, weeksMoney[weekNum] ?? 0}
                }
            };
            
            Labels = new[] { "Week" + (weekNum - 4), "Week" + (weekNum - 3), "Week" + (weekNum - 2), "Week" +(weekNum-1),"Week" + weekNum  };
            YFormatter = value => value+"DKK";

     

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}