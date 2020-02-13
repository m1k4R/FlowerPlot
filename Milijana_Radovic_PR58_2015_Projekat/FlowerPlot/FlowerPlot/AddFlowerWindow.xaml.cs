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
    /// Interaction logic for AddFlowerWindow.xaml
    /// </summary>
    public partial class AddFlowerWindow : Window
    {
        private string name;

        public AddFlowerWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            name = txtName.Text;

            if (!String.IsNullOrEmpty(name))
            {
                Flower newFlower = new Flower { Name = name };

                if (ConnectionService.Instance.proxy.AddNewFlower(newFlower))
                {
                    MainWindow.logMessage = "Added a new flower.";
                    LogService.Instance.LogInformation("Added a new flower.");
                    LogService.Instance.SendServerInformation("Added a new flower.");
                    this.Close();
                }
                else
                {
                    MainWindow.logMessage = "Error while adding a new flower.";
                    LogService.Instance.LogError("Error while adding a new flower.");
                    LogService.Instance.SendServerError("Error while adding a new flower.");
                    MessageBox.Show("Enter a flower name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MainWindow.logMessage = "Error while adding a new flower.";
                LogService.Instance.LogError("Error while adding a new flower.");
                LogService.Instance.SendServerError("Error while adding a new flower.");
                MessageBox.Show("Enter a flower name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.logMessage = "Canceled adding a new flower.";
            LogService.Instance.LogInformation("Canceled adding a new flower.");
            LogService.Instance.SendServerInformation("Canceled adding a new flower.");
            this.Close();
        }
    }
}
