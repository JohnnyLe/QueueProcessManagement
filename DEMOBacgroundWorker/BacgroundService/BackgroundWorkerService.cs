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
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Timers;

namespace DEMOBacgroundWorker
{
    /// <summary>
    /// </summary>
    public class BackgroundWorkerService
    {
        static BackgroundWorkerService _instance;
        static readonly object _lock = new object();
        static bool isStart = false;
        System.Timers.Timer worker;
        double timeElapsed = 1000;// 1 second
        
        
        /// <summary>
        /// delegate do work
        /// </summary>
        /// <param name="fullPathBlackList"></param>
        public delegate void DoworkDelegate(MessageAction message);
        public event DoworkDelegate DoworkDelegateFunc;


        /// <summary>
        /// BlackListAddInChecker Instance
        /// </summary>
        /// <returns></returns>
        public static BackgroundWorkerService Instance()
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new BackgroundWorkerService();
                return _instance;
            }
        }

        public BackgroundWorkerService()
        {
            // Init worker
            worker = new System.Timers.Timer(timeElapsed);
            worker.Elapsed += new ElapsedEventHandler(DoWork);
        }

        /// <summary>
        /// DoWork
        /// </summary>
        private void DoWork(object sender, ElapsedEventArgs e)
        {
            if (!QueueMessageManager.Instance().IsEmpty() && DoworkDelegateFunc != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    MessageAction message = QueueMessageManager.Instance().GetMessage();
                    DoworkDelegateFunc(message);
                });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (!isStart)
            {
                try
                {
                    worker.Start();
                    isStart = true;
                }
                catch (Exception ex)
                {
                    //Logging.Instance.Error("start Exception ", ex);
                }
            }
            else
                System.Diagnostics.Debug.WriteLine("**** WK is running ****");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (isStart)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("**** STOP WK ****");
                    isStart = false;
                    worker.Stop();
                }
                catch (Exception ex)
                {
                    //Logging.Instance.Error("WK stop exeption", ex);
                }
            }
        }

    }
}
