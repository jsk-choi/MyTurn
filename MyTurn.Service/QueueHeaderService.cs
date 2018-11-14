using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public class QueueHeaderService : IQueueHeaderService
    {
        //QueueHeader Get(string Id);
        //Task<QueueHeader> AddUpdate(QueueHeader queueHeader);

        public async Task<QueueHeader> AddUpdate(QueueHeader queueHeader)
        {
            using (var ctx = new MyTurnDb()) {

                var thisQueueHeader = await ctx.QueueHeader.FindAsync(queueHeader.Id);

                if (thisQueueHeader == null)
                {
                    queueHeader.CreateDate = DateTime.Now;
                    ctx.QueueHeader.Add(queueHeader);
                    await ctx.SaveChangesAsync();
                    return queueHeader;
                }
                else
                {
                    thisQueueHeader = queueHeader;
                    await ctx.SaveChangesAsync();
                    return thisQueueHeader;
                }
            }
        }

        public async Task<QueueHeader> Get(int Id)
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.QueueHeader.FindAsync(Id);
            }
        }
    }
}
