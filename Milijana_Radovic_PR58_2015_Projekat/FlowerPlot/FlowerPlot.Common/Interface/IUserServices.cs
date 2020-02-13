using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static FlowerPlot.Common.Enums;

namespace FlowerPlot.Common
{
    [ServiceContract]
    public interface IUserServices
    {
        [OperationContract]
        User GetUser(string username);
        [OperationContract]
        bool AddNewUser(User user);
        [OperationContract]
        bool EditUser(User user);
        [OperationContract]
        Plot AddNewFlowerPlot(Plot flowerPlot);
        [OperationContract]
        IEnumerable<Plot> GetFlowerPlots();
        [OperationContract]
        bool AddNewFlower(Flower flower);
        [OperationContract]
        bool AddNewSoilType(SoilType soilType);
        [OperationContract]
        bool DeleteFlowerPlot(Plot flowerPlot);
        [OperationContract]
        IEnumerable<Flower> GetFlowers();
        [OperationContract]
        IEnumerable<SoilType> GetSoilTypes();
        [OperationContract]
        bool EditFlowerPlot(Plot flowerPlot);
        //[OperationContract]
        //bool EditFlower(Flower flower);
        //[OperationContract] 
        //bool EditSoilType(SoilType soilType);
        [OperationContract]
        IEnumerable<Plot> SearchByStage(FlowerPlotStages stage);
        [OperationContract]
        bool DuplicateFlowerPlot(Plot flowerPlot);
        [OperationContract]
        Plot GetFlowerPlot(int id);
        [OperationContract]
        void LogInformation(string message);
        [OperationContract]
        void LogError(string message);
    } 
}
