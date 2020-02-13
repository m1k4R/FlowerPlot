using FlowerPlot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot
{
    public class ConnectionService
    {
        public IUserServices proxy;
        private static ConnectionService _instance;

        public static ConnectionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConnectionService();
                }

                return _instance;
            }
        }

        public ConnectionService()
        {
            ChannelFactory<IUserServices> channelFactory = new ChannelFactory<IUserServices>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10101/IUserServices"));
            proxy = channelFactory.CreateChannel();
        }
    }
}
