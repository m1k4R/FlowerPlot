using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.LogServices 
{
    public class LogService : ILogService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static LogService _instance;
        public static List<string> logApp = new List<string>(); 

        public LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public static LogService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogService();

                return _instance;
            }
        }

        public void LogError(string message)
        {
            logApp.Add($"Error: {message} - {DateTime.Now}");
            log.Error(message);
        }

        public void LogInformation(string message)
        {
            logApp.Add($"Information: {message} - {DateTime.Now}");
            log.Info(message);
        }

        public void SendServerError(string message)
        {
            ConnectionService.Instance.proxy.LogError(message);
        }

        public void SendServerInformation(string message)
        {
            ConnectionService.Instance.proxy.LogInformation(message);
        }
    }
}
