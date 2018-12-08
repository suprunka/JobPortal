using AppJobPortal.TcpUserReference;
using LiveCharts;
using LiveCharts.Defaults;
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
    /// Interaction logic for UsersGender.xaml
    /// </summary>
    public partial class UsersGender : UserControl
    {
        private IUserService _userproxy;
        public UsersGender()
        {
            InitializeComponent();
            _userproxy = new UserServiceClient("UserServiceTcpEndpoint");
            var users = _userproxy.GetAll();


            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Male",
                    Values = new ChartValues<ObservableValue> { new ObservableValue((int)users.Where(x=>x.Gender.Equals(JobPortal.Model.Gender.Male)).Count()) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Female",
                    Values = new ChartValues<ObservableValue> { new ObservableValue((int)users.Where(x=>x.Gender.Equals(JobPortal.Model.Gender.Female)).Count())  },
                    DataLabels = true
                }
              
            };

            //adding values or series will update and animate the chart automatically
            //SeriesCollection.Add(new PieSeries());
            //SeriesCollection[0].Values.Add(5);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
    }
}
