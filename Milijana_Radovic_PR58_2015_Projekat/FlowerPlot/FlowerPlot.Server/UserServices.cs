using FlowerPlot.Common;
using FlowerPlot.Server.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server
{
    class UserServices : IUserServices
    {
        public bool AddNewFlower(Flower flower)
        {
            return FlowerPlotsDBManager.Instance.AddNewFlower(flower);
        }

        public Plot AddNewFlowerPlot(Plot flowerPlot)
        {
            return FlowerPlotsDBManager.Instance.AddFlowerPlot(flowerPlot);
        }

        public bool AddNewSoilType(SoilType soilType)
        {
            return FlowerPlotsDBManager.Instance.AddNewSoilType(soilType);
        }

        public bool AddNewUser(User user)
        {
            return UsersDBManager.Instance.AddUser(user);
        }

        public bool DeleteFlowerPlot(Plot flowerPlot)
        {
            return FlowerPlotsDBManager.Instance.RemoveFlowerPlot(flowerPlot);
        }

        public bool DuplicateFlowerPlot(Plot flowerPlot)
        {
            return FlowerPlotsDBManager.Instance.DuplicateFlowerPlot(flowerPlot);
        }

        public bool EditFlowerPlot(Plot flowerPlot)
        {
            return FlowerPlotsDBManager.Instance.EditFlowerPlot(flowerPlot);
        }

        public bool EditUser(User user)
        {
            return UsersDBManager.Instance.EditUser(user);
        }

        public Plot GetFlowerPlot(int id)
        {
            return FlowerPlotsDBManager.Instance.GetFlowerPlot(id);
        }

        public IEnumerable<Plot> GetFlowerPlots()
        {
            return FlowerPlotsDBManager.Instance.GetFlowerPlots();
        }

        public IEnumerable<Flower> GetFlowers()
        {
            return FlowerPlotsDBManager.Instance.GetFlowers();
        }

        public IEnumerable<SoilType> GetSoilTypes()
        {
            return FlowerPlotsDBManager.Instance.GetSoilTypes();
        }

        public User GetUser(string username)
        {
            return UsersDBManager.Instance.FindUser(username);
        }

        public void LogError(string message)
        {
            Console.WriteLine($"Error: {message} - {DateTime.Now}");
        }

        public void LogInformation(string message) 
        {
            Console.WriteLine($"Information: {message} - {DateTime.Now}");
        }

        public IEnumerable<Plot> SearchByStage(Enums.FlowerPlotStages stage)
        {
            return FlowerPlotsDBManager.Instance.GetFlowerPlots().Where(c => c.Stage == stage);
        }
    }
}
