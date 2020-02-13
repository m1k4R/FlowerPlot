using FlowerPlot.Common;
using FlowerPlot.Server.Access;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            Init();

            ConnectionService _connectionService = new ConnectionService();
            _connectionService.StartConnection();
            Console.ReadKey(true);
            _connectionService.EndConnection();
        }

        private static void Init()
        {
            UserServices service = new UserServices();
            if (service.AddNewUser(new User { Username = "admin", Name = "admin", Password = "admin", Surname = "admin", Role = "admin" }))
            {
                Console.WriteLine("Admin was added.");
            }
            else
            {
                Console.WriteLine("Admin is already in the DB.");
            }
            
            var plots = FlowerPlotsDBManager.Instance.GetFlowerPlots();
            if (plots.Count() >= 8)
            {
                Console.WriteLine("Flower plots are already in the DB.");
                return;
            }
            else
            {
                ImportComponents();
            }
        }

        private static void ImportComponents()
        {
            UserServices service = new UserServices();
            service.AddNewUser(new User { Name = "Milijana", Surname = "Radovic", Username = "mika", Password = "mika", Role = "customer" });
            service.AddNewUser(new User { Name = "Svjetlana", Surname = "Radovic", Username = "ceca", Password = "ceca", Role = "customer" });
            service.AddNewFlowerPlot(new Plot { Area = 27, MoisturePerc = 0, PlantingDate = "", HarvestDate = "", Stage = Enums.FlowerPlotStages.Empty, StageImage = "", Flower = new Flower { Name = "" }, Soil = new SoilType { ClayPercent = 0, SandPercent = 0, SiltPercent = 0, Name = "" } });
            service.AddNewFlowerPlot(new Plot { Area = 25, MoisturePerc = 32, PlantingDate = "9/10/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Flowering, StageImage = "../Images/s27.png", Flower = new Flower { Name = "Rose" }, Soil = new SoilType { ClayPercent = 50, SandPercent = 20, SiltPercent = 30, Name = "Cl50Sa20Si30"} });
            service.AddNewFlowerPlot(new Plot { Area = 15, MoisturePerc = 17, PlantingDate = "9/3/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Germination, StageImage = "../Images/s25.png", Flower = new Flower { Name = "Tulip" }, Soil = new SoilType { ClayPercent = 25, SandPercent = 25, SiltPercent = 50, Name = "Cl25Sa25Si50" } });
            service.AddNewFlowerPlot(new Plot { Area = 45, MoisturePerc = 27, PlantingDate = "9/10/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Seed, StageImage = "../Images/s24.png", Flower = new Flower { Name = "Orchid" }, Soil = new SoilType { ClayPercent = 15, SandPercent = 45, SiltPercent = 40, Name = "Cl15Sa45Si40" } });
            service.AddNewFlowerPlot(new Plot { Area = 33, MoisturePerc = 30, PlantingDate = "9/4/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Seed, StageImage = "../Images/s24.png", Flower = new Flower { Name = "Iris" }, Soil = new SoilType { ClayPercent = 20, SandPercent = 20, SiltPercent = 60, Name = "Cl20Sa20Si60" } });
            service.AddNewFlowerPlot(new Plot { Area = 50, MoisturePerc = 23, PlantingDate = "9/10/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Growth, StageImage = "../Images/s26.png", Flower = new Flower { Name = "Lavender" }, Soil = new SoilType { ClayPercent = 60, SandPercent = 20, SiltPercent = 20, Name = "Cl60Sa20Si20" } });
            service.AddNewFlowerPlot(new Plot { Area = 14, MoisturePerc = 23, PlantingDate = "9/8/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Flowering, StageImage = "../Images/s27.png", Flower = new Flower { Name = "Violet" }, Soil = new SoilType { ClayPercent = 30, SandPercent = 30, SiltPercent = 30, Name = "Cl30Sa30Si30" } });
            service.AddNewFlowerPlot(new Plot { Area = 46, MoisturePerc = 32, PlantingDate = "9/17/2018", HarvestDate = "", Stage = Enums.FlowerPlotStages.Growth, StageImage = "../Images/s26.png", Flower = new Flower { Name = "Primrose" }, Soil = new SoilType { ClayPercent = 70, SandPercent = 15, SiltPercent = 15, Name = "Cl70Sa15Si15" } });
           
        }

    }
}