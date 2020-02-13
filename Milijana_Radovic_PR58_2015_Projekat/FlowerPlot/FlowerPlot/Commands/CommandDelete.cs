using FlowerPlot.Common;
using FlowerPlot.LogServices;
using FlowerPlot.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlowerPlot.Commands 
{
    public class CommandDelete : Command
    {
        public CommandDelete(Plot flowerPlot) : base(flowerPlot) { }

        public override void Execute()
        {
            if (flowerPlot != null)
            {
                if (ConnectionService.Instance.proxy.DeleteFlowerPlot(flowerPlot))
                {
                    var result = ConnectionService.Instance.proxy.DeleteFlowerPlot(flowerPlot);
                    MainWindow.logMessage = "Deleted selected flower plot.";
                    LogService.Instance.LogInformation("Deleted selected flower plot.");
                    LogService.Instance.SendServerInformation("Deleted selected flower plot.");
                }
                else
                {
                    MessageBox.Show("Error redo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow.logMessage = "Error while deleting selected flower plot.";
                    LogService.Instance.LogError("Selected flower plot is already deleted.");
                    LogService.Instance.SendServerError("Selected flower plot is already deleted.");
                }
            }
        }

        public override void Unexecute()
        {
            Plot newPlot = ConnectionService.Instance.proxy.AddNewFlowerPlot(flowerPlot);
            MainWindow.logMessage = "Undo.";
            LogService.Instance.LogInformation("Undo.");
            LogService.Instance.SendServerInformation("Undo.");
        }
    }
}
