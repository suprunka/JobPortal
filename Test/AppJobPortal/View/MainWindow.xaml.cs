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

            }

            private void btnNuevo_Click(object sender, RoutedEventArgs e)
            {
                ActivarText();
                Activarbtns();
            }
            private void btnModificar_Click(object sender, RoutedEventArgs e)
            {
                Activarbtns();
                ActivarText();
            }

            private void btnEliminar_Click(object sender, RoutedEventArgs e)
            {

            }
            private void btnGuardar_Click(object sender, RoutedEventArgs e)
            {
                Desactivarbtns();
                DesactivarText();
            }

            private void btnCancelar_Click(object sender, RoutedEventArgs e)
            {
                Desactivarbtns();
                DesactivarText();
            }
            private void Activarbtns()
            {
                btnPanelone.Visibility = Visibility.Hidden;
                btnPaneltwo.Visibility = Visibility.Visible;
            }
            private void Desactivarbtns()
            {
                btnPaneltwo.Visibility = Visibility.Hidden;
                btnPanelone.Visibility = Visibility.Visible;
            }
            private void ActivarText()
            {


            txtAddress.IsEnabled = true;
                txtCity.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtFname.IsEnabled = true;
                txtLname.IsEnabled = true;
                txtPhonenumber.IsEnabled = true;
                //txtFind.IsEnabled = false;
                lvClients.IsEnabled = false;
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
            lvClients.IsEnabled = true;
            }
        }
    }
