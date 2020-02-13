using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Common
{
    [DataContract]
    public class SoilType : INotifyPropertyChanged
    {
        //private String name;

        public event PropertyChangedEventHandler PropertyChanged;
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public int ClayPercent { get; set; }
        [DataMember]
        public int SandPercent { get; set; }
        [DataMember]
        public int SiltPercent { get; set; }

    }
}
