﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service.Interface
{
    interface IQueueDetailService
    {
        QueueDetail AddUpdate(QueueDetail vendor);
    }
}