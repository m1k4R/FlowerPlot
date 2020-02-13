using FlowerPlot.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static FlowerPlot.Common.Enums;

namespace FlowerPlot.Common
{
    [DataContract]
    public class Plot : FlowerPlotPrototype, INotifyPropertyChanged
    {
        //private Flower flower;
        //private SoilType soil;

        public event PropertyChangedEventHandler PropertyChanged;
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember] 
        public float Area { get; set; }
        [DataMember]
        public float MoisturePerc { get; set; }
        [DataMember]
        public string HarvestDate { get; set; }
        [DataMember]
        public string PlantingDate { get; set; }
        [DataMember]
        public FlowerPlotStages Stage { get; set; }
        [DataMember]
        public Flower Flower { get; set; }
        [DataMember]
        public SoilType Soil { get; set; }
        [DataMember]
        public string StageImage { get; set; }

        public override FlowerPlotPrototype Clone()
        {
            Plot newFlowerPlot = new Plot
            {
                Area = this.Area,
                MoisturePerc = this.MoisturePerc,
                PlantingDate = this.PlantingDate,
                HarvestDate = this.HarvestDate,
                Flower = this.Flower,
                Soil = this.Soil,
                Stage = this.Stage,
                StageImage = this.StageImage
            };

            return newFlowerPlot;
        }
    }
}
