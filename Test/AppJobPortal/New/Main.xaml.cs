using System.ComponentModel;
using System.Windows;

namespace AppJobPortal.New
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            
        }

        private void Users_Clicked(object sender, RoutedEventArgs e)
        {

            DataContext = new AppJobPortal.New.Users();
        }
       
        private void Services_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AppJobPortal.New.Services();
        }
    }
}
