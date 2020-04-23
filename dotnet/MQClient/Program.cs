using System;
using IBM.WMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpusMagus.IBM.MQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = AppConfig.BuildServiceProvider(); // Simply used to get configuration and DI container if you like
            var config = serviceProvider.GetService<IConfiguration>();
            MQClientFacade.SetupMQEnvironment(config);
            MQClientFacade.MQPut(config);
            MQClientFacade.MQGet(config);
        }
    }
}
