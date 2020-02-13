using FlowerPlot.ViewModel;
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

namespace FlowerPlot.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            this.DataContext = new FlowerPlot.ViewModel.ProfileViewModel();

            txtUsername.IsReadOnly = true;
            txtPassword.IsReadOnly = true;
            txtName.IsReadOnly = true;
            txtSurname.IsReadOnly = true;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            txtName.IsReadOnly = false;
            txtSurname.IsReadOnly = false;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnChange.Visibility = Visibility.Hidden;

            txtName.BorderBrush = Brushes.White;
            txtName.BorderThickness = new Thickness(2);
            txtSurname.BorderBrush = Brushes.White;
            txtSurname.BorderThickness = new Thickness(2);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileViewModel.canSaveChanges)
            {
                txtName.IsReadOnly = true;
                txtSurname.IsReadOnly = true;
                btnSave.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                btnChange.Visibility = Visibility.Visible;

                txtName.BorderBrush = Brushes.Transparent;
                txtSurname.BorderBrush = Brushes.Transparent;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtName.IsReadOnly = true;
            txtSurname.IsReadOnly = true;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnChange.Visibility = Visibility.Visible;

            txtName.BorderBrush = Brushes.Transparent;
            txtSurname.BorderBrush = Brushes.Transparent;
        }
    }
}
