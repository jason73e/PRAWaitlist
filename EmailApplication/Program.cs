﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MailQueue mq = new MailQueue();
            mq.ProcessQueue();
        }
    }
}
