using FlowerPlot.Common;
using FlowerPlot.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private AddUserViewModel addUserViewModel = new AddUserViewModel();
        private ProfileViewModel profileViewModel = new ProfileViewModel();
        private NewFlowerPlotViewModel newFlowerPlotViewModel = new NewFlowerPlotViewModel();
        private FlowerPlotsViewModel flowerPlotsViewModel = new FlowerPlotsViewModel();
        private LogViewModel logViewModel = new LogViewModel();
        private BindableBase currentViewModel;

        private static MainWindowViewModel _instance;

        public static MainWindowViewModel Instance {
            get
            {
                if (_instance == null)
                    _instance = new MainWindowViewModel();

                return _instance;
            }
        }

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = flowerPlotsViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public void OnNav(string destination)
        {
            
            switch (destination)
            {
                case "addUser":
                    CurrentViewModel = addUserViewModel;
                    break;
                case "viewProfile":
                    CurrentViewModel = profileViewModel;
                    break;
               case "newIteration":
                    CurrentViewModel = newFlowerPlotViewModel;
                    break;
               case "flowerPlots":
                    CurrentViewModel = flowerPlotsViewModel;
                    break;
               case "change":
                    CurrentViewModel = newFlowerPlotViewModel;
                    break;
                case "log":
                    CurrentViewModel = logViewModel;
                    break;
            }
            
        }
    
    }
}


