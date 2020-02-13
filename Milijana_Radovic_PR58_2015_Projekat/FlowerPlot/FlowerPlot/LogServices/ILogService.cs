using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.LogServices 
{
    public interface ILogService
    {
        void LogInformation(string message);
        void LogError(string message);
        void SendServerInformation(string message);
        void SendServerError(string message);
    } 
}
