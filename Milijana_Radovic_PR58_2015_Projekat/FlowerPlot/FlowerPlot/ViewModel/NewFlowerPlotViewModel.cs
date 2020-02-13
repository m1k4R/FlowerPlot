using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static FlowerPlot.Common.Enums;

namespace FlowerPlot.ViewModel
{
    public class NewFlowerPlotViewModel : BindableBase, INotifyPropertyChanged
    {
        private string area;
        private string moisturePerc;
        private string plantingDate;
        private string harvestDate;
        private FlowerPlotStages stage;
        private string stageImage;
        private int clay;
        private int sand;
        private int silt;

        private List<Flower> flowers;
        private List<SoilType> soilTypes;
        private Flower selectedFlower;
        private SoilType selectedSoilType;

        public MyICommand AddFlowerCommand { get; set; }
        public MyICommand AddSoilTypeCommand { get; set; }
        public MyICommand StartCommand { get; set; }
        public MyICommand SeedStageCommand { get; set; }
        public MyICommand GerminationStageCommand { get; set; }
        public MyICommand GrowthStageCommand { get; set; }
        public MyICommand FloweringStageCommand { get; set; }
        public MyICommand CutCommand { get; set; }

        public bool canStart = false;
        public bool canSeed = false;
        public bool canGermination = false;
        public bool canGrowth = false;
        public bool canFlowering = false;
        public bool canCut = false;

        //FlowerPlotsViewModel flowerPlotsViewModel = new FlowerPlotsViewModel();

        public string Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
                OnPropertyChanged("Area");
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        public string MoisturePerc
        {
            get
            {
                return moisturePerc;
            }
            set
            {
                moisturePerc = value;
                OnPropertyChanged("MoisturePerc");
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        public string PlantingDate
        {
            get
            {
                return plantingDate;
            }
            set
            {
                plantingDate = value;
                OnPropertyChanged("PlantingDate");
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        public string HarvestDate
        {
            get
            {
                return harvestDate;
            }
            set
            {
                harvestDate = value;
                OnPropertyChanged("HarvestDate");
                CutCommand.RaiseCanExecuteChanged();
            }
        }

        public FlowerPlotStages Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
                OnPropertyChanged("Stage");
            }
        }

        public string StageImage
        {
            get
            {
                return stageImage;
            }
            set
            {
                stageImage = value;
                OnPropertyChanged("StageImage");
            }
        }

        public int Clay
        {
            get
            {
                return clay;
            }
            set
            {
                clay = value;
                OnPropertyChanged("Clay");
            }
        }

        public int Sand
        {
            get
            {
                return sand;
            }
            set
            {
                sand = value;
                OnPropertyChanged("Sand");
            }
        }

        public int Silt
        {
            get
            {
                return silt;
            }
            set
            {
                silt = value;
                OnPropertyChanged("Silt");
            }
        }

        public List<Flower> Flowers
        {
            get
            {
                return flowers;
            }
            set
            {
                flowers = value;
                OnPropertyChanged("Flowers");
            }
        }

        public List<SoilType> SoilTypes
        {
            get
            {
                return soilTypes;
            }
            set
            {
                soilTypes = value;
                OnPropertyChanged("SoilTypes");
            }
        }

        public Flower SelectedFlower
        {
            get
            {
                return selectedFlower;
            }
            set
            {
                if (selectedFlower != value)
                {
                    selectedFlower = value;
                    OnPropertyChanged("SelectedFlower");
                    StartCommand.RaiseCanExecuteChanged();
                }
            }
        }


        public SoilType SelectedSoilType
        {
            get
            {
                return selectedSoilType;
            }
            set
            {
                if (selectedSoilType != value)
                {
                    selectedSoilType = value;
                    OnPropertyChanged("SelectedSoilType");
                    if (SelectedSoilType != null)
                    {
                        Clay = selectedSoilType.ClayPercent;
                        Sand = selectedSoilType.SandPercent;
                        Silt = selectedSoilType.SiltPercent;
                        StartCommand.RaiseCanExecuteChanged();
                    }
                    else
                    {
                        Clay = 0;
                        Sand = 0;
                        Silt = 0;
                    }
                }
            }
        }

        public NewFlowerPlotViewModel()
        {
            Flowers = new List<Flower>();
            SoilTypes = new List<SoilType>();

            AddFlowerCommand = new MyICommand(AddFlower);
            AddSoilTypeCommand = new MyICommand(AddSoilType);
            StartCommand = new MyICommand(StartIteration, CanStart);
            SeedStageCommand = new MyICommand(SeedStage); //CanSeedStage
            GerminationStageCommand = new MyICommand(GerminationStage); //CanGerminationStage
            GrowthStageCommand = new MyICommand(GrowthStage); //CanGrowthStage
            FloweringStageCommand = new MyICommand(FloweringStage); //CanFloweringStage
            CutCommand = new MyICommand(Cut); //CanCut

            Initialize();


        }

        private void Initialize()
        {
            Flowers = new List<Flower>(ConnectionService.Instance.proxy.GetFlowers());
            SoilTypes = new List<SoilType>(ConnectionService.Instance.proxy.GetSoilTypes());

            if (MainWindow.selectedPlot != null)
            {
                Area = MainWindow.selectedPlot.Area.ToString();
                MoisturePerc = MainWindow.selectedPlot.MoisturePerc.ToString();
                PlantingDate = MainWindow.selectedPlot.PlantingDate;
                HarvestDate = MainWindow.selectedPlot.HarvestDate;
                Stage = MainWindow.selectedPlot.Stage;
                StageImage = MainWindow.selectedPlot.StageImage;
                SelectedFlower = MainWindow.selectedPlot.Flower;
                SelectedSoilType = MainWindow.selectedPlot.Soil;
                Clay = MainWindow.selectedPlot.Soil.ClayPercent;
                Sand = MainWindow.selectedPlot.Soil.SandPercent;
                Silt = MainWindow.selectedPlot.Soil.SiltPercent;

                switch (stage)
                {
                    case FlowerPlotStages.Empty:
                        canStart = true;
                        break;
                    case FlowerPlotStages.Seed:
                        canGermination = true;
                        break;
                    case FlowerPlotStages.Germination:
                        canGrowth = true;
                        break;
                    case FlowerPlotStages.Growth:
                        canFlowering = true;
                        break;
                    case FlowerPlotStages.Flowering:
                        canCut = true;
                        break;
                }

                CanChangeStage();
            }
        }

        private void AddFlower()
        {
            AddFlowerWindow addFlower = new AddFlowerWindow();
            addFlower.ShowDialog();
            Initialize();
        }

        private void AddSoilType()
        {
            AddSoilTypeWindow addSoilType = new AddSoilTypeWindow();
            addSoilType.ShowDialog();
            Initialize();
        }

        private Plot CreateFlowerPlot()
        {
            Plot newFlowerPlot = new Plot
            {
                Area = float.Parse(Area),
                MoisturePerc = float.Parse(MoisturePerc),
                PlantingDate = PlantingDate,
                HarvestDate = HarvestDate,
                Flower = SelectedFlower,
                Soil = SelectedSoilType,
                Stage = Stage,
                StageImage = StageImage,
            };

            return newFlowerPlot;
        }

        private void CanChangeStage()
        {
            if (canCut)
            {
                CutCommand.RaiseCanExecuteChanged();
            }
            else if (canFlowering)
            {
                FloweringStageCommand.RaiseCanExecuteChanged();
            }
            else if (canGrowth)
            {
                GrowthStageCommand.RaiseCanExecuteChanged();
            }
            else if (canGermination)
            {
                GerminationStageCommand.RaiseCanExecuteChanged();
            }
            else if (canSeed)
            {
                SeedStageCommand.RaiseCanExecuteChanged();
            }
            else if (canStart)
            {
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private bool Conflict()
        {
            Plot plotInDb = ConnectionService.Instance.proxy.GetFlowerPlot(MainWindow.selectedPlot.Id);

            if (plotInDb == null)
            {
                MainWindow.logMessage = "Conflict while changing the flower plot.";
                LogService.Instance.LogInformation("Conflict while changing the flower plot.");
                LogService.Instance.SendServerInformation("Conflict while changing the flower plot.");
                var result = MessageBox.Show("Do you want to discard your changes?", "Conflict", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    Stage = FlowerPlotStages.Empty;
                    StageImage = "";
                    Plot newFlowerPlot = CreateFlowerPlot();
                    newFlowerPlot.Id = MainWindow.selectedPlot.Id;
                    Plot newPlot = ConnectionService.Instance.proxy.AddNewFlowerPlot(newFlowerPlot);
                    MainWindow.selectedPlot = newPlot;
                    canSeed = true;

                    return false;
                }
            }
            else if (plotInDb.Stage != Stage)
            {
                MainWindow.logMessage = "Conflict while changing the flower plot.";
                LogService.Instance.LogInformation("Conflict while changing the flower plot.");
                LogService.Instance.SendServerInformation("Conflict while changing the flower plot.");
                var result = MessageBox.Show("Do you want to discard your changes?", "Conflict", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void StartIteration()
        {
            Stage = FlowerPlotStages.Empty;
            StageImage = "";

            Plot newFlowerPlot = CreateFlowerPlot();

            if (MainWindow.selectedPlot == null)
            {
                Plot newPlot = ConnectionService.Instance.proxy.AddNewFlowerPlot(newFlowerPlot);
                MainWindow.selectedPlot = newPlot;
                canSeed = true;
                MainWindow.logMessage = "Added a new flower plot.";
                LogService.Instance.LogInformation("Added a new flower plot.");
                LogService.Instance.SendServerInformation("Added a new flower plot.");
            }
            else
            {
                if (!Conflict())
                {
                    newFlowerPlot.Id = MainWindow.selectedPlot.Id;

                    if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                    {
                        canSeed = true;
                        MainWindow.logMessage = "Started a new iteration.";
                        LogService.Instance.LogInformation("Started a new iteration.");
                        LogService.Instance.SendServerInformation("Started a new iteration.");
                    }
                    else
                    {
                        MainWindow.logMessage = "Error while starting a new iteration.";
                        LogService.Instance.LogError("Error while starting a new iteration.");
                        LogService.Instance.SendServerError("Error while starting a new iteration.");
                        MessageBox.Show("Error starting new iteration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            if (canSeed)
            {
                CanChangeStage();
            }

            
        }

        private bool CanStart()
        {
            return !String.IsNullOrEmpty(area) && !String.IsNullOrEmpty(moisturePerc);
        }

        private void SeedStage()
        {
            if (!Conflict())
            {
                Stage = FlowerPlotStages.Seed;
                StageImage = "../Images/s24.png";

                Plot newFlowerPlot = CreateFlowerPlot();

                newFlowerPlot.Id = MainWindow.selectedPlot.Id;

                if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                {
                    canGermination = true;
                    CanChangeStage();
                    MainWindow.logMessage = "Change stage: seed.";
                    LogService.Instance.LogInformation("Change stage: seed.");
                    LogService.Instance.SendServerInformation("Change stage: seed.");
                }
                else
                {
                    MainWindow.logMessage = "Error while changing stage: seed.";
                    LogService.Instance.LogError("Error while changing stage: seed.");
                    LogService.Instance.SendServerError("Error while changing stage: seed.");
                    MessageBox.Show("Error seed stage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GerminationStage()
        {
            if (!Conflict())
            {
                Stage = FlowerPlotStages.Germination;
                StageImage = "../Images/s25.png";

                Plot newFlowerPlot = CreateFlowerPlot();

                newFlowerPlot.Id = MainWindow.selectedPlot.Id;

                if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                {
                    canGrowth = true;
                    CanChangeStage();
                    MainWindow.logMessage = "Change stage: germination.";
                    LogService.Instance.LogInformation("Change stage: germination.");
                    LogService.Instance.SendServerInformation("Change stage: germination.");
                }
                else
                {
                    MainWindow.logMessage = "Error while changing stage: germination.";
                    LogService.Instance.LogError("Error while changing stage: germination.");
                    LogService.Instance.SendServerError("Error while changing stage: germination.");
                    MessageBox.Show("Error germination stage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GrowthStage()
        {
            if (!Conflict())
            {
                Stage = FlowerPlotStages.Growth;
                StageImage = "../Images/s26.png";

                Plot newFlowerPlot = CreateFlowerPlot();

                newFlowerPlot.Id = MainWindow.selectedPlot.Id;

                if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                {
                    canFlowering = true;
                    CanChangeStage();
                    MainWindow.logMessage = "Change stage: growth.";
                    LogService.Instance.LogInformation("Change stage: growth.");
                    LogService.Instance.SendServerInformation("Change stage: growth.");
                }
                else
                {
                    MainWindow.logMessage = "Error while changing stage: growth.";
                    LogService.Instance.LogError("Error while changing stage: growth.");
                    LogService.Instance.SendServerError("Error while changing stage: growth.");
                    MessageBox.Show("Error growth stage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FloweringStage()
        {
            if (!Conflict())
            {
                Stage = FlowerPlotStages.Flowering;
                StageImage = "../Images/s27.png";

                Plot newFlowerPlot = CreateFlowerPlot();

                newFlowerPlot.Id = MainWindow.selectedPlot.Id;

                if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                {
                    canCut = true;
                    CanChangeStage();
                    //flowerPlotsViewModel.Refresh();
                    MainWindow.logMessage = "Change stage: flowering.";
                    LogService.Instance.LogInformation("Change stage: flowering.");
                    LogService.Instance.SendServerInformation("Change stage: flowering.");
                }
                else
                {
                    MainWindow.logMessage = "Error while changing stage: flowering.";
                    LogService.Instance.LogError("Error while changing stage: flowering.");
                    LogService.Instance.SendServerError("Error while changing stage: flowering.");
                    MessageBox.Show("Error flowering stage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Cut()
        {
            if (!Conflict())
            {
                Stage = FlowerPlotStages.Empty;
                StageImage = "";
                MoisturePerc = "0";
                SelectedFlower = null;
                SelectedSoilType = null;
                PlantingDate = "";
                HarvestDate = "";


                Plot newFlowerPlot = CreateFlowerPlot();

                newFlowerPlot.Id = MainWindow.selectedPlot.Id;
                newFlowerPlot.Flower = new Flower { Name = "", Id = 1 };
                newFlowerPlot.Soil = new SoilType { Name = "", Id = 1, ClayPercent = 0, SandPercent = 0, SiltPercent = 0 };

                if (ConnectionService.Instance.proxy.EditFlowerPlot(newFlowerPlot))
                {
                    canStart = false;
                    canSeed = false;
                    canGermination = false;
                    canGrowth = false;
                    canFlowering = false;
                    canCut = false;
                    MainWindow.logMessage = "Cut flowers.";
                    LogService.Instance.LogInformation("Cut flowers.");
                    LogService.Instance.SendServerInformation("Cut flowers.");
                }
                else
                {
                    MainWindow.logMessage = "Error while cutting flowers.";
                    LogService.Instance.LogError("Error while cutting flowers.");
                    LogService.Instance.SendServerError("Error while cutting flowers.");
                    MessageBox.Show("Error cut.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
