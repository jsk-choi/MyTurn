using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public interface IQueueHeaderService
    {
        Task<QueueHeader> AddUpdate(QueueHeader queueHeader);
        Task<QueueHeader> Get(int Id);
    }
}
