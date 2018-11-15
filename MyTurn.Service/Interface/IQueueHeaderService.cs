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
        Task<IList<QueueHeader>> Get();
        Task<IList<QueueHeader>> Get(int VendorId);
    }
}
