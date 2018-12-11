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
using JobPortal.Model;
using AppJobPortal.New;
using System.Threading.Tasks;
using System.Threading;
namespace AppJobPortal

{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        private IOfferService _offerproxy;
        private AllServices services;
        private EarnedMoney earned;
        private UserBoughtServices boughtServices;
        private UsersGender gender;
        private ServicesByRegionandRate regionandRate;
        private Top10Services top10;
        public Statistics()
        {

            InitializeComponent();
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");

            DataContext = new AllServices();
            Init();
        }

        private void Init()
        {
            services = new AllServices();
            earned = new EarnedMoney();
            boughtServices = new UserBoughtServices();
            gender = new UsersGender();
            regionandRate = new ServicesByRegionandRate();
            top10 = new Top10Services();
        }


        private  void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() => {
                Init();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            Task t = new Task(() =>
            {
                thread.Start();
                thread.Join();
            });
            t.Start();
            DataContext = services;
        }

        private async void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            EarnedMoney all = null;
            await Task.Run(() =>
            {

                Dispatcher.Invoke(() =>
                {
                    all = new EarnedMoney();
                    DataContext = all;
                });
            });
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new UserBoughtServices();
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new UsersGender();
        }
        private void Button_Click_5(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new ServicesByRegionandRate();
        }
        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new Top10Services();
        }
    }
}