using AppJobPortal.UserServiceReferenceTcp;
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

namespace AppJobPortal.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

        public partial class MainWindow : Window
        {
            public MainWindow()
            {
            InitializeComponent();
            ActiveTextBoxes();
            regionBox.ItemsSource = Enum.GetValues(typeof(Region)).Cast<Region>();


        }



        private void btnRgister_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Activarbtns()
            {
                btnPaneltwo.Visibility = Visibility.Visible;
            }
            private void Desactivarbtns()
            {
                btnPaneltwo.Visibility = Visibility.Hidden;
            }
            private void ActiveTextBoxes()
            {
           
                txtAddress.IsEnabled = true;
                txtCity.IsEnabled = true;
                txtPostcode.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtFname.IsEnabled = true;
                txtLname.IsEnabled = true;
                txtPhonenumber.IsEnabled = true;
                txbBirthDateLbl.IsEnabled = true;
            
            //txtFind.IsEnabled = false;
                usersTable.IsEnabled = true; ;
            }
            private void DesactivarText()
            {
            txtAddress.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtFname.IsEnabled = false;
            txtLname.IsEnabled = false;
            txtPhonenumber.IsEnabled = false;
            //txtFind.IsEnabled = true;
            usersTable.IsEnabled = true;
            }
        }
    }
