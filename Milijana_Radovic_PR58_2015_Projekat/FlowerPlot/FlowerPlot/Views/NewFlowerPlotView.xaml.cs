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
using static FlowerPlot.Common.Enums;

namespace FlowerPlot.Views
{
    /// <summary>
    /// Interaction logic for NewFlowerPlotView.xaml
    /// </summary>
    public partial class NewFlowerPlotView : UserControl
    {
        public NewFlowerPlotView()
        {
            InitializeComponent();
            this.DataContext = new FlowerPlot.ViewModel.NewFlowerPlotViewModel();

            imgRightArrow.Visibility = Visibility.Hidden;
            btnCut.Visibility = Visibility.Hidden;

            btnSeedState.IsEnabled = false;
            btnGerminationState.IsEnabled = false;
            btnGrowthState.IsEnabled = false;
            btnFloweringState.IsEnabled = false;
            
            if (MainWindow.selectedPlot != null)
            {
                txtArea.IsReadOnly = true;
                cmbFlower.Text = MainWindow.selectedPlot.Flower.Name;
                cmbSoil.Text = MainWindow.selectedPlot.Soil.Name;
                FlowerPlotStages stage = MainWindow.selectedPlot.Stage;

                switch (stage)
                {
                    case FlowerPlotStages.Empty:
                        btnStart.Background = Brushes.ForestGreen;
                        break;
                    case FlowerPlotStages.Seed:
                        btnStart.Background = Brushes.ForestGreen;
                        btnSeedState.Background = Brushes.ForestGreen;
                        btnSeedState.IsEnabled = true;
                        btnGerminationState.IsEnabled = true;
                        break;
                    case FlowerPlotStages.Germination:
                        btnStart.Background = Brushes.ForestGreen;
                        btnSeedState.Background = Brushes.ForestGreen;
                        btnGerminationState.Background = Brushes.ForestGreen;
                        btnSeedState.IsEnabled = true;
                        btnGerminationState.IsEnabled = true;
                        btnGrowthState.IsEnabled = true;
                        break;
                    case FlowerPlotStages.Growth:
                        btnStart.Background = Brushes.ForestGreen;
                        btnSeedState.Background = Brushes.ForestGreen;
                        btnGerminationState.Background = Brushes.ForestGreen;
                        btnGrowthState.Background = Brushes.ForestGreen;
                        btnSeedState.IsEnabled = true;
                        btnGerminationState.IsEnabled = true;
                        btnGrowthState.IsEnabled = true;
                        btnFloweringState.IsEnabled = true;
                        break;
                    case FlowerPlotStages.Flowering:
                        btnStart.Background = Brushes.ForestGreen;
                        btnSeedState.Background = Brushes.ForestGreen;
                        btnGerminationState.Background = Brushes.ForestGreen;
                        btnGrowthState.Background = Brushes.ForestGreen;
                        btnFloweringState.Background = Brushes.ForestGreen;
                        imgRightArrow.Visibility = Visibility.Visible;
                        btnCut.Visibility = Visibility.Visible;
                        btnSeedState.IsEnabled = true;
                        btnGerminationState.IsEnabled = true;
                        btnGrowthState.IsEnabled = true;
                        btnFloweringState.IsEnabled = true;
                        break;
                }
            }
        }

        private void btnSeedState_Click(object sender, RoutedEventArgs e)
        {
            btnSeedState.Background = Brushes.ForestGreen;
            btnGerminationState.IsEnabled = true;
        }

        private void btnGerminationState_Click(object sender, RoutedEventArgs e)
        {
            btnGerminationState.Background = Brushes.ForestGreen;
            btnGrowthState.IsEnabled = true;
        }

        private void btnGrowthState_Click(object sender, RoutedEventArgs e)
        {
            btnGrowthState.Background = Brushes.ForestGreen;
            btnFloweringState.IsEnabled = true;
        }

        private void btnFloweringState_Click(object sender, RoutedEventArgs e)
        {
            btnFloweringState.Background = Brushes.ForestGreen;
            imgRightArrow.Visibility = Visibility.Visible;
            btnCut.Visibility = Visibility.Visible;
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            btnSeedState.Background = Brushes.DarkGray;
            btnGerminationState.Background = Brushes.DarkGray;
            btnGrowthState.Background = Brushes.DarkGray;
            btnFloweringState.Background = Brushes.DarkGray;
            imgRightArrow.Visibility = Visibility.Hidden;
            btnCut.Visibility = Visibility.Hidden;
            btnSeedState.IsEnabled = false;
            btnGerminationState.IsEnabled = false;
            btnGrowthState.IsEnabled = false;
            btnFloweringState.IsEnabled = false;
            // btnCut.Background = Brushes.ForestGreen;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.Background = Brushes.ForestGreen;
            btnSeedState.IsEnabled = true;
        }
    }
}
