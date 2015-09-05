/// <summary>
/// Simple demo to create queue management for handling mutiple process and scale
/// Author: https://github.com/JohnnyLe 
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEMOBacgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.  Set delegate workder function
            ImplementDoWorkFunction implement=new ImplementDoWorkFunction();
            BackgroundWorkerService.Instance().DoworkDelegateFunc += implement.ImplementFunctionDowork;
            // 2. Start Worker => When worker running the worker will get each message from Queue to process
            BackgroundWorkerService.Instance().Start();

            // 3. Demo how it work to handle multiple concurrency
            // Create sample pull client request 
            // Each MessageAction is example for each Client request.
            // MessageAction is the class object that will contain most Client request infomation 
            // to use to process ansync in Background worker
            MessageAction item1 = new MessageAction("1", "XXX1");
            MessageAction item2 = new MessageAction("2", "XXX2");
            MessageAction item3 = new MessageAction("3", "XXX3");
            MessageAction item4 = new MessageAction("4", "XXX4");

            // Test retry failed with this item
            // Example item5 always send failed, and just allow retry maximun 3 times
            MessageAction item5 = new MessageAction("5", "XXX5");
            // In this case Server do not process immediately Client request, all request will be put into Queue
            QueueMessageManager.Instance().AddMessage(item1);
            QueueMessageManager.Instance().AddMessage(item2);
            QueueMessageManager.Instance().AddMessage(item3);
            QueueMessageManager.Instance().AddMessage(item4);
            QueueMessageManager.Instance().AddMessage(item5);

            Console.ReadLine();



        }
    }
}
