using FlowerPlot.Common;
using FlowerPlot.LogServices;
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
using System.Windows.Shapes;

namespace FlowerPlot
{
    /// <summary>
    /// Interaction logic for AddSoilTypeWindow.xaml
    /// </summary>
    public partial class AddSoilTypeWindow : Window
    {
        private int clay;
        private int sand;
        private int silt;

        public AddSoilTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // validacija da se moraju unijeti brojevi
            clay = Int32.Parse(txtClay.Text);
            sand = Int32.Parse(txtSand.Text);
            silt = Int32.Parse(txtSilt.Text);

            string name = "Cl" + clay + "Sa" + sand + "Si" + silt;
            int sum = clay + sand + silt;

            if (!String.IsNullOrEmpty(txtClay.Text) && !String.IsNullOrEmpty(txtSand.Text) && !String.IsNullOrEmpty(txtSilt.Text) && sum == 100)
            {
                SoilType newSoilType = new SoilType { ClayPercent = clay, SandPercent = sand, SiltPercent = silt, Name = name };

                if (ConnectionService.Instance.proxy.AddNewSoilType(newSoilType))
                {
                    MainWindow.logMessage = "Added a new soil type.";
                    LogService.Instance.LogInformation("Added a new soil type.");
                    LogService.Instance.SendServerInformation("Added a new soil type.");
                    this.Close();
                }
                else
                {
                    MainWindow.logMessage = "Error while adding a new soil type.";
                    LogService.Instance.LogError("Error while adding a new soil type.");
                    LogService.Instance.SendServerError("Error while adding a new soil type.");
                    MessageBox.Show("Enter a numbers (sum = 100)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MainWindow.logMessage = "Error while adding a new soil type.";
                LogService.Instance.LogError("Error while adding a new soil type.");
                LogService.Instance.SendServerError("Error while adding a new soil type.");
                MessageBox.Show("Enter a numbers (sum = 100)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.logMessage = "Canceled adding a new soil type.";
            LogService.Instance.LogInformation("Canceled adding a new soil type.");
            LogService.Instance.SendServerInformation("Canceled adding a new soil type.");
            this.Close();
        }
    }
}
