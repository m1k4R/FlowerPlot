using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FlowerPlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string username;
        private string password;
        public static User currentUser = null;
        public static Plot selectedPlot = null;
        public static string logMessage;

        public MainWindow()
        {
            DataContext = MainWindowViewModel.Instance;
            //DataContext = this;
            InitializeComponent();

            gridActions.Visibility = Visibility.Hidden;
            gridView.Visibility = Visibility.Hidden;
            gridLogin.Visibility = Visibility.Visible;
            imgHome.Visibility = Visibility.Hidden;
            btnHome.Visibility = Visibility.Hidden;
            btnLog.Visibility = Visibility.Hidden;
            logLabel.Visibility = Visibility.Hidden;

            new Thread(() =>
            {
                while (true)
                {
                    InvokeMethod();
                    Thread.Sleep(100);
                }
            }).Start();
        }

        private void InvokeMethod()
        {
            Dispatcher.Invoke(() =>
            {
                logLabel.Content = logMessage;
            });
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Text;

            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(username))
            {
                User customer = ConnectionService.Instance.proxy.GetUser(username);

                if (customer == null)
                {
                    MessageBox.Show("Customer does not exist");
                    return;
                }

                if (customer.Password.Equals(password))
                {
                    currentUser = customer;
                    gridLogin.Visibility = Visibility.Hidden;
                    gridActions.Visibility = Visibility.Visible;
                    gridView.Visibility = Visibility.Visible;
                    imgHome.Visibility = Visibility.Hidden;
                    btnHome.Visibility = Visibility.Hidden;
                    btnLog.Visibility = Visibility.Visible;
                    logLabel.Visibility = Visibility.Visible;

                    if (currentUser.Role == "admin")
                    {
                        btnAddUser.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnAddUser.Visibility = Visibility.Hidden;
                    }

                    LogService.Instance.LogInformation("User logged in.");
                    LogService.Instance.SendServerInformation("User logged in.");
                    logMessage = "You are logged in.";
                    //logLabel.Content = logMessage;
                    MainWindowViewModel.Instance.OnNav("flowerPlots");
                }
                else
                {
                    LogService.Instance.LogError("Wrong password or username.");
                    LogService.Instance.SendServerError("Wrong password or username.");
                    MessageBox.Show("Wrong Password");
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            gridActions.Visibility = Visibility.Hidden;
            gridView.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Hidden;
            btnHome.Visibility = Visibility.Hidden;
            gridLogin.Visibility = Visibility.Visible;
            txtUsername.Text = "";
            txtPassword.Text = "";
            btnLog.Visibility = Visibility.Hidden;
            logLabel.Visibility = Visibility.Hidden;
            LogService.Instance.LogInformation("User logged out.");
            LogService.Instance.SendServerInformation("User logged out.");
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            gridActions.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Visible;
            btnHome.Visibility = Visibility.Visible;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Visible;
            logLabel.Visibility = Visibility.Visible;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            selectedPlot = null;
            gridActions.Visibility = Visibility.Visible;
            imgHome.Visibility = Visibility.Hidden;
            btnHome.Visibility = Visibility.Hidden;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Visible;
            logLabel.Visibility = Visibility.Visible;
        }

        private void btnViewProfile_Click(object sender, RoutedEventArgs e)
        {
            gridActions.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Visible;
            btnHome.Visibility = Visibility.Visible;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Visible;
            logLabel.Visibility = Visibility.Visible;
        }

        private void btnNewIteration_Click(object sender, RoutedEventArgs e)
        {
            selectedPlot = null;
            gridActions.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Visible;
            btnHome.Visibility = Visibility.Visible;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Visible;
            logLabel.Visibility = Visibility.Visible;
        }

        private void btnFlowerPlots_Click(object sender, RoutedEventArgs e)
        {
            selectedPlot = null;
            gridActions.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Visible;
            btnHome.Visibility = Visibility.Visible;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Visible;
            logLabel.Visibility = Visibility.Visible;
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            gridActions.Visibility = Visibility.Hidden;
            imgHome.Visibility = Visibility.Visible;
            btnHome.Visibility = Visibility.Visible;
            gridView.Visibility = Visibility.Visible;
            btnLog.Visibility = Visibility.Hidden;
            logLabel.Visibility = Visibility.Visible;
        }
    }
}
