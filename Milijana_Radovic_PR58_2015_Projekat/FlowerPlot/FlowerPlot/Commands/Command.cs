using FlowerPlot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Commands 
{
    public abstract class Command
    {
        protected Plot flowerPlot;

        public Command(Plot flowerPlot)
        {
            this.flowerPlot = flowerPlot;
        }

        public abstract void Execute();
        public abstract void Unexecute();
    }
}
