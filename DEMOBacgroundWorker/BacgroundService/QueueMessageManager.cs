/// <summary>
/// Simple demo to create queue management for handling mutiple process and scale
/// Author: https://github.com/JohnnyLe 
/// </summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Timers;


namespace DEMOBacgroundWorker
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueMessageManager
    {
        static QueueMessageManager _instance;
        static readonly object _lock = new object();
        
        /// <summary>
        /// QueueMessageManager Instance
        /// </summary>
        /// <returns></returns>
        public static QueueMessageManager Instance()
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new QueueMessageManager();
                return _instance;
            }
        }

        public QueueMessageManager()
        {
            // Init queue
            Queue = new Queue<MessageAction>();
            
        }

        /// <summary>
        /// Queue
        /// </summary>
        private Queue<MessageAction> Queue;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddMessage(MessageAction item)
        {
            Queue.Enqueue(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MessageAction GetMessage()
        {
            return Queue.Dequeue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Queue.Count > 0)
                return false;
            else
                return true;
        }
    }
}
