using FlowerPlot.Commands;
using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static FlowerPlot.Common.Enums;

namespace FlowerPlot.ViewModel
{
    public class FlowerPlotsViewModel : BindableBase, INotifyPropertyChanged
    {
        private  ObservableCollection<Plot> flowerPlots;
        private Plot selectedFlowerPlot;

        private List<FlowerPlotStages> stages;
        private FlowerPlotStages selectedStage;  

        public MyICommand DeleteCommand { get; set; }
        public MyICommand ChangeCommand { get; set; }
        public MyICommand SearchCommand { get; set; }
        public MyICommand ShowAllCommand { get; set; }
        public MyICommand DuplicateCommand { get; set; }
        public MyICommand UndoCommand { get; set; }
        public MyICommand RedoCommand { get; set; } 

        public static int currentCommand;
        public static List<Command> commands;

        public List<FlowerPlotStages> Stages
        {
            get
            {
                return stages;
            }
            set
            {
                stages = value;
                OnPropertyChanged("Stages");
            }
        }

        public FlowerPlotStages SelectedStage
        {
            get
            {
                return selectedStage;
            }
            set
            {
                if (selectedStage != value)
                {
                    selectedStage = value;
                    OnPropertyChanged("SelectedStage");
                    SearchCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<Plot> FlowerPlots
        {
            get
            {
                return flowerPlots;
            }
            set
            {
                flowerPlots = value;
                OnPropertyChanged("FlowerPlots");
            }
        }

        public Plot SelectedFlowerPlot
        {
            get
            {
                return selectedFlowerPlot;
            }
            set
            {
                if (selectedFlowerPlot != value)
                {
                    selectedFlowerPlot = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    ChangeCommand.RaiseCanExecuteChanged();
                    DuplicateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public FlowerPlotsViewModel()
        {
            DeleteCommand = new MyICommand(Delete, CanDelete);
            ChangeCommand = new MyICommand(Change, CanChange);
            SearchCommand = new MyICommand(Search);
            ShowAllCommand = new MyICommand(ShowAll);
            DuplicateCommand = new MyICommand(Duplicate, CanDuplicate);
            UndoCommand = new MyICommand(Undo);
            RedoCommand = new MyICommand(Redo);

            FlowerPlots = new ObservableCollection<Plot>();
            Stages = new List<FlowerPlotStages>();
            SelectedStage = FlowerPlotStages.Empty;

            commands = new List<Command>();

            Initialize();
            new Thread(() =>
            {
                while (true)
                {
                    Refresh();
                    Thread.Sleep(5000);
                }
            }).Start();
            
        }

        public void Initialize()
        {
            FlowerPlots = new ObservableCollection<Plot>(ConnectionService.Instance.proxy.GetFlowerPlots());
            SelectedFlowerPlot = null;
            Stages = new List<FlowerPlotStages> { FlowerPlotStages.Empty, FlowerPlotStages.Seed, FlowerPlotStages.Germination, FlowerPlotStages.Growth, FlowerPlotStages.Flowering };
        }

        public void Refresh()
        {
            lock (flowerPlots)
            {
                FlowerPlots = new ObservableCollection<Plot>(ConnectionService.Instance.proxy.GetFlowerPlots());
            }
        }

        private void Delete()
        {
            Command command = new CommandDelete(SelectedFlowerPlot);
            command.Execute();
            if (commands.Count > currentCommand)
            {
                commands.RemoveRange(currentCommand, commands.Count - currentCommand);
            }
            commands.Add(command);
            currentCommand++;

            //if (ConnectionService.Instance.proxy.DeleteFlowerPlot(SelectedFlowerPlot))
            //{
            //    var result = ConnectionService.Instance.proxy.DeleteFlowerPlot(SelectedFlowerPlot); 
            //    MainWindow.logMessage = "Deleted selected flower plot.";
            //    LogService.Instance.LogInformation("Deleted selected flower plot.");
            //    LogService.Instance.SendServerInformation("Deleted selected flower plot.");
            //}
            //else
            //{
            //    MessageBox.Show("Selected item is allready deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    MainWindow.logMessage = "Error while deleting selected flower plot.";
            //    LogService.Instance.LogError("Selected flower plot is already deleted.");
            //    LogService.Instance.SendServerError("Selected flower plot is already deleted.");
            //}
        }

        private bool CanDelete()
        {
            return (selectedFlowerPlot != null);
        }

        private void Change()
        {
            MainWindow.selectedPlot = SelectedFlowerPlot;
            MainWindowViewModel.Instance.OnNav("newIteration");
        }

        private bool CanChange()
        {
            return (selectedFlowerPlot != null);
        }

        private void Search()
        {
            FlowerPlots = new ObservableCollection<Plot>(ConnectionService.Instance.proxy.SearchByStage(SelectedStage));
            MainWindow.logMessage = $"Searching for stage: {SelectedStage}.";
            LogService.Instance.LogInformation($"Searching for {SelectedStage} stage");
            LogService.Instance.SendServerInformation($"Searching for {SelectedStage} stage");
        }

        private void ShowAll()
        {
            FlowerPlots = new ObservableCollection<Plot>(ConnectionService.Instance.proxy.GetFlowerPlots());
        }

        private void Duplicate()
        {
            Command command = new CommandDuplicate(SelectedFlowerPlot);
            command.Execute();
            if (commands.Count > currentCommand)
            {
                commands.RemoveRange(currentCommand, commands.Count - currentCommand);
            }
            commands.Add(command);
            currentCommand++;
            //Plot duplicate = (Plot)selectedFlowerPlot.Clone();
            //ConnectionService.Instance.proxy.DuplicateFlowerPlot(duplicate);
            //MainWindow.logMessage = "Duplicated selected flower plot.";
            //LogService.Instance.LogInformation("Duplicated selected flower plot.");
            //LogService.Instance.SendServerInformation("Duplicated selected flower plot.");
        }

        private bool CanDuplicate()
        {
            return (selectedFlowerPlot != null);
        }

        private void Undo()
        {
            if (currentCommand > 0)
            {
                Command command = commands[--currentCommand];
                command.Unexecute();
            }
        }

        private void Redo()
        {
            if (currentCommand < commands.Count) //currentCommand + 1
            {
                MainWindow.logMessage = "Redo.";
                LogService.Instance.LogInformation("Redo.");
                LogService.Instance.SendServerInformation("Redo.");
                Command command = commands[currentCommand++];
                command.Execute();
            }
        }
    }
}
