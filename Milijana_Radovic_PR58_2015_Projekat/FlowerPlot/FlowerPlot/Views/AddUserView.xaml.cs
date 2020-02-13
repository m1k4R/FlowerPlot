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
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : UserControl
    {
        public AddUserView()
        {
            InitializeComponent();
            this.DataContext = new FlowerPlot.ViewModel.AddUserViewModel();

            txtName.Text = "";
            txtSurname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            MainWindowViewModel.Instance.OnNav("flowerPlots");
        }
    }
}
