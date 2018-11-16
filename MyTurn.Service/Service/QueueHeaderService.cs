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
        public async Task<QueueHeader> AddUpdate(QueueHeader queueHeader)
        {
            using (var ctx = new MyTurnDb()) {

                var thisQueueHeader = await ctx.QueueHeader.FindAsync(queueHeader.Id);

                if (thisQueueHeader == null)
                {
                    queueHeader.CreateDate = DateTime.Now;
                    ctx.QueueHeader.Add(queueHeader);
                    var task = await ctx.SaveChangesAsync();
                    return queueHeader;
                }
                else
                {
                    thisQueueHeader = queueHeader;
                    var task = await ctx.SaveChangesAsync();
                    return thisQueueHeader;
                }
            }
        }

        public async Task<IList<QueueHeader>> Get()
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.QueueHeader
                    .Where(x => x.Active)
                    .ToListAsync();
            }
        }

        public async Task<IList<QueueHeader>> Get(int VendorId)
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.QueueHeader
                    .Where(x => x.VendorId == VendorId)
                    .ToListAsync();
            }
        }
    }
}
