using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Common
{
    public interface IFlowerPlotsDBManager
    {
        Plot AddFlowerPlot(Plot flowerPlot);
        IEnumerable<Plot> GetFlowerPlots();
        bool AddNewFlower(Flower flower);
        bool AddNewSoilType(SoilType soilType);
        bool RemoveFlowerPlot(Plot flowerPlot);
        IEnumerable<Flower> GetFlowers();
        IEnumerable<SoilType> GetSoilTypes();
        bool EditFlowerPlot(Plot flowerPlot);
        //bool EditFlower(Flower flower);
        //bool EditSoilType(SoilType soilType);
        bool DuplicateFlowerPlot(Plot flowerPlot);
        Plot GetFlowerPlot(int id);
    }
}
