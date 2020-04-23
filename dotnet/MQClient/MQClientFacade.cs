using System;
using IBM.WMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpusMagus.IBM.MQClient
{
    class MQClientFacade
    {
        internal static void MQPut(IConfiguration config)
        {
            SetupMQEnvironment(config);
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

        internal static void MQGet(IConfiguration config)
        {
            SetupMQEnvironment(config);
            var qmgrName = config.GetValue<string>("QueueManagerName");
            Console.WriteLine($"Trying to connect to Queue Manager {qmgrName}...");
            var qmgr = new MQQueueManager(qmgrName);
            Console.WriteLine($"Connected to Queue Manager {qmgrName}.");
            var qName = "DEV.QUEUE.1";
            Console.WriteLine($"Reading message from queue {qName}.");
            int mqQueueOpenOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT;
            var retrievedMessage = new MQMessage();
            qmgr.AccessQueue(qName,mqQueueOpenOptions).Get(retrievedMessage);
            var messageContents = retrievedMessage.ReadString(retrievedMessage.MessageLength);
            Console.WriteLine($"Message read from queue {qName}.");
        }

        internal static void SetupMQEnvironment(IConfiguration config)
        {
            MQEnvironment.Hostname = config.GetValue<string>("MQEnvironment.Hostname");
            MQEnvironment.Port = config.GetValue<int>("MQEnvironment.Port");
            MQEnvironment.UserId = config.GetValue<string>("MQEnvironment.UserId");
            MQEnvironment.Password = config.GetValue<string>("MQEnvironment.Password");
            MQEnvironment.Channel = config.GetValue<string>("MQEnvironment.Channel");
        }
    }
}
