using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public class QueueDetailService : IQueueDetailService
    {
        public async Task<QueueDetail> AddUpdate(QueueDetail queueDetail)
        {
            using (var ctx = new MyTurnDb()) {

                var thisQueueDetail = await ctx.QueueDetail.FindAsync(queueDetail.Id);

                if (thisQueueDetail == null)
                {
                    var maxSort = ctx.QueueDetail
                        .Where(x => x.QueueHeaderId == queueDetail.QueueHeaderId)
                        .Select(x => (decimal?)x.Sort)
                        .Max();

                    queueDetail.CreateDate = DateTime.Now;
                    queueDetail.Sort = (maxSort ?? 0) + 1;

                    ctx.QueueDetail.Add(queueDetail);
                    var task = await ctx.SaveChangesAsync();
                    return queueDetail;
                }
                else
                {
                    thisQueueDetail = queueDetail;
                    var task = await ctx.SaveChangesAsync();
                    return thisQueueDetail;
                }
            }
        }

        public async Task<IList<QueueDetail>> Get(int queueHeaderId)
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.QueueDetail
                    .Where(x => x.QueueStatusId == queueHeaderId)
                    .OrderBy(x => x.Sort)
                    .ToListAsync();
            }
        }
    }
}
