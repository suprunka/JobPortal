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
            _offerproxy = new OfferServiceClient();
           
            DataContext = new AllServices();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new AllServices();

        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new EarnedMoney();

        }
    }
}