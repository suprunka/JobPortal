using System.ComponentModel;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace AppJobPortal.New
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Services _services;
        private Statistics _statistics;
        private Users _users;
        public Main()
        {
            _services = new AppJobPortal.New.Services();
            _users = new AppJobPortal.New.Users();
            _statistics = new AppJobPortal.Statistics();
        }


        private void Users_Clicked(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
                       {
                           DataContext = _users;
                       });

        }

        private  void Services_Clicked(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                DataContext = _services;
            });
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                DataContext = _statistics;
            });
        }
    }
}
