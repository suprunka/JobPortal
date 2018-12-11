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

        public Statistics()
        {

            InitializeComponent();
            _offerproxy = new OfferServiceClient("OfferServiceTcpEndpoint");

            DataContext = new AllServices();
        }


        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AllServices all = null;
            await Task.Run(() =>
            {

                Dispatcher.Invoke(() =>
                {
                    all = new AllServices();
                    DataContext = all;
                });
            });
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