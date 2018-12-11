using System.ComponentModel;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace AppJobPortal.New
{

    public partial class Main : Window
    {

        public Main()
        {

        }


        private void Users_Clicked(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
                       {
                           DataContext = new Users();
                       }));

        }

        private  void Services_Clicked(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                DataContext = new Services();
            }));
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                DataContext = new Statistics();
            }));
        }
    }
}
