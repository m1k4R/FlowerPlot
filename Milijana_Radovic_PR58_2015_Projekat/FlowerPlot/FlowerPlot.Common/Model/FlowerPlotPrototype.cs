using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Common.Model
{
    [DataContract]
    public abstract class FlowerPlotPrototype
    {
        public abstract FlowerPlotPrototype Clone();
    }
}
