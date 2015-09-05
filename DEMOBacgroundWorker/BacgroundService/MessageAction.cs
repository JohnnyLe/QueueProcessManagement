/// <summary>
/// Simple demo to create queue management for handling mutiple process and scale
/// Author: https://github.com/JohnnyLe 
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEMOBacgroundWorker
{
    /// <summary>
    /// MessageAction is the class object that will contain most Client request infomation to use to process ansync in Background worker
    /// </summary>
    public class MessageAction
    {
        /// <summary>
        /// 
        /// </summary>
        public String ID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public String Info { set; get; }

        public MessageAction() { }

        public MessageAction(String id, String info)
        {
            this.ID = id;
            this.Info = info;
        }

        public int CurrentFailedToReTry = 0;
        public bool StausFailed = false;


        //More other pros


    }
}
