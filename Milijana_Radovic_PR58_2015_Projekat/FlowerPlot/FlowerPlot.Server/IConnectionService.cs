using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server
{
    interface IConnectionService
    {
        void StartConnection();
        void EndConnection();
    }
}
