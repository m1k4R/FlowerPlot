using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlowerPlot.Commands
{
    public class CommandDuplicate : Command
    {
        public CommandDuplicate(Plot flowerPlot) : base(flowerPlot) { }

        public override void Execute()
        {
            if (flowerPlot != null)
            {
                Plot duplicate = (Plot)flowerPlot.Clone();
                ConnectionService.Instance.proxy.DuplicateFlowerPlot(duplicate);
                MainWindow.logMessage = "Duplicated selected flower plot.";
                LogService.Instance.LogInformation("Duplicated selected flower plot.");
                LogService.Instance.SendServerInformation("Duplicated selected flower plot.");
            }
        }

        public override void Unexecute()
        {
            if (flowerPlot != null)
            {
                if (ConnectionService.Instance.proxy.DeleteFlowerPlot(flowerPlot))
                {
                    var result = ConnectionService.Instance.proxy.DeleteFlowerPlot(flowerPlot);
                    MainWindow.logMessage = "Undo.";
                    LogService.Instance.LogInformation("Undo.");
                    LogService.Instance.SendServerInformation("Undo.");
                }
                else
                {
                    MessageBox.Show("Error undo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow.logMessage = "Error undo.";
                    LogService.Instance.LogError("Error undo.");
                    LogService.Instance.SendServerError("Error undo.");
                }
            }
        }
    }
}
