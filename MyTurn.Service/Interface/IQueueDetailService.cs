using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public interface IQueueDetailService
    {
        Task<QueueDetail> AddUpdate(QueueDetail queueDetail);
        Task<IList<QueueDetail>> Get(int queueHeaderId);
        Task<QueueDetail> Get(int queueHeaderId, int personId);
        Task<bool> IsInLine(int queueHeaderId, int personId);
    }
}
