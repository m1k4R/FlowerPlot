using FlowerPlot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FlowerPlot.Server.Access
{
    public class FlowerPlotsDBManager : IFlowerPlotsDBManager
    {
        private static FlowerPlotsDBManager _instance;
        private FlowerPlotsDBManager() { }
        public static FlowerPlotsDBManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FlowerPlotsDBManager();

                return _instance;
            }
        }

        public Plot AddFlowerPlot(Plot flowerPlot)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                Plot addedFlowerPlot = dbContext.FlowerPlots.Add(flowerPlot);
                dbContext.SaveChanges();

                return addedFlowerPlot;
            }
        }

        public bool AddNewFlower(Flower flower)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                Flower addedFlower = dbContext.Flowers.Add(flower);
                dbContext.SaveChanges();

                return true;
            }
        }

        public bool AddNewSoilType(SoilType soilType)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                SoilType addedSoilType = dbContext.SoilTypes.Add(soilType);
                dbContext.SaveChanges();

                return true;
            }
        }

        public bool DuplicateFlowerPlot(Plot flowerPlot)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                flowerPlot.Id = dbContext.FlowerPlots.Count() + 1;
                flowerPlot.Flower = dbContext.Flowers.FirstOrDefault(c => c.Id == flowerPlot.Flower.Id);
                flowerPlot.Soil = dbContext.SoilTypes.FirstOrDefault(c => c.Id == flowerPlot.Soil.Id);
                Plot plot = dbContext.FlowerPlots.Add(flowerPlot);
                dbContext.SaveChanges();

                return true;
            }
        }

        public bool EditFlowerPlot(Plot flowerPlot)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                var isInDb = dbContext.FlowerPlots.Any(c => c.Id == flowerPlot.Id);
                if (isInDb)
                {
                    var plot = dbContext.FlowerPlots.FirstOrDefault(c => c.Id == flowerPlot.Id);
                    plot.Area = flowerPlot.Area;
                    plot.MoisturePerc = flowerPlot.MoisturePerc;
                    //plot.Flower = flowerPlot.Flower;
                    plot.Flower = dbContext.Flowers.FirstOrDefault(c => c.Id == flowerPlot.Flower.Id);
                    //plot.Soil = flowerPlot.Soil;
                    plot.Soil = dbContext.SoilTypes.FirstOrDefault(c => c.Id == flowerPlot.Soil.Id);
                    plot.PlantingDate = flowerPlot.PlantingDate;
                    plot.HarvestDate = flowerPlot.HarvestDate;
                    plot.Stage = flowerPlot.Stage;
                    plot.StageImage = flowerPlot.StageImage;

                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        

        public Plot GetFlowerPlot(int id)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                return dbContext.FlowerPlots.FirstOrDefault(p => p.Id == id);
            }
        }

        public IEnumerable<Plot> GetFlowerPlots()
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                var flowerPlots = dbContext.FlowerPlots
                                .Include(plot => plot.Flower).Include(plot => plot.Soil)
                                .ToList();
                return flowerPlots;
            }
        }

        public IEnumerable<Flower> GetFlowers()
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                return dbContext.Flowers.ToList();
            }
        }

        public IEnumerable<SoilType> GetSoilTypes()
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                return dbContext.SoilTypes.ToList();
            }
        }

        public bool RemoveFlowerPlot(Plot flowerPlot)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {

                bool isInDb = dbContext.FlowerPlots.Any(c => c.Id == flowerPlot.Id);

                if (isInDb)
                {
                    var remove = dbContext.FlowerPlots.FirstOrDefault(c => c.Id == flowerPlot.Id);

                    dbContext.FlowerPlots.Remove(remove);

                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
