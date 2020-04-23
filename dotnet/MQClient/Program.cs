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
            MQPut(config);
        }

        public static void MQPut(IConfiguration config)
        {
            MQEnvironment.Hostname = config.GetValue<string>("MQEnvironment.Hostname");
            MQEnvironment.Port = config.GetValue<int>("MQEnvironment.Port");
            MQEnvironment.UserId = config.GetValue<string>("MQEnvironment.UserId");
            MQEnvironment.Password = config.GetValue<string>("MQEnvironment.Password");
            MQEnvironment.Channel = config.GetValue<string>("MQEnvironment.Channel");
            var qmgrName = config.GetValue<string>("QueueManagerName");
            Console.WriteLine($"Trying to connect to Queue Manager {qmgrName}...");
            var qmgr = new MQQueueManager(qmgrName);
            Console.WriteLine($"Connected to Queue Manager {qmgrName}.");
            var qName = "DEV.QUEUE.1";
            Console.WriteLine($"Putting message on queue {qName}.");
            var mqMessage = new MQMessage();
            mqMessage.WriteString("This is a demo message");
            qmgr.Put(qName, mqMessage);
            Console.WriteLine($"Message put on queue {qName}.");
        }
    }
}
