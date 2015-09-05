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
    public class ImplementDoWorkFunction
    {
        private int maxRetryFail = 3;
        // This is the mail function process send message to client.
        public void ImplementFunctionDowork(MessageAction message)
        {
            if (message.StausFailed)
            {
                message.CurrentFailedToReTry ++;
                Console.WriteLine("Re try send last fail message, ID=" + message.ID + " |number of fail retry: " + message.CurrentFailedToReTry.ToString() + " |Max time to retry"+ maxRetryFail.ToString());
            }
            //TODO 
            // This implementation to process message Queue
            Console.WriteLine("Worker is processing do job send message: "+message.ID +"-"+message.Info);

            // Test example send failed message then add back to queue to retry
            SimulateSendFail(message);

        }

        private void SimulateSendFail(MessageAction message)
        {
            // Example simulate meesage ID=5 always send failed
            if (message.ID.Equals("5") && message.CurrentFailedToReTry < 3)
            {
                message.StausFailed = true;
                QueueMessageManager.Instance().AddMessage(message);             
            }
        }
    }
}
