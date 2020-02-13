using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.ViewModel
{
    public class LogViewModel : BindableBase, INotifyPropertyChanged
    {
        private List<string> logList;

        public LogViewModel()
        {
            LogList = new List<string>();
            LogList = LogService.logApp;
        }

        public List<string> LogList
        {
            get
            {
                return logList;
            }

            set
            {
                if (logList != value)
                {
                    logList = value;
                    OnPropertyChanged("LogList");
                }
            }
        }
    }
}
