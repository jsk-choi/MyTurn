using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AutoMapper;

using MyTurn.Db;
using MyTurn.Service;

using ef = MyTurn.Db;
using dto = MyTurn.Web.Models;
using System.Threading.Tasks;

namespace MyTurn.Web.Api
{
    public class QueueDetailController : ApiController
    {
        private readonly IQueueDetailService QueueDetailService;

        public QueueDetailController(IQueueDetailService _queueDetailService) {
            QueueDetailService = _queueDetailService;
        }

        // GET: api/QueueDetail/5
        public async Task<IHttpActionResult> Get(int queueHeaderId)
        {
            var queueDetails = await QueueDetailService.Get(queueHeaderId);
            var queueDetailsDto = Mapper.Map<IList<dto.QueueDetail>>(queueDetails);
            return Ok(queueDetailsDto);
        }

        public async Task<IHttpActionResult> Get(int queueHeaderId, int queueDetailId)
        {
            var queueDetails = await QueueDetailService.Get(queueHeaderId);
            var queueDetailsDto = Mapper.Map<IList<dto.QueueDetail>>(queueDetails);
            return Ok(queueDetailsDto);
        }

        // POST: api/QueueDetail
        public async Task<IHttpActionResult> Post([FromBody]dto.QueueDetail queueDetail)
        {
            var isInLine = await QueueDetailService.IsInLine(queueDetail.QueueHeaderId, queueDetail.PersonId);

            if (isInLine) {
                return BadRequest("Already in line.");
            }

            var queueDetailEf = Mapper.Map<QueueDetail>(queueDetail);
            var queueDetailNew = await QueueDetailService.AddUpdate(queueDetailEf);
            var queueDetailDto = Mapper.Map<dto.QueueDetail>(queueDetailNew);

            return Ok(queueDetailDto);
        }

        // PUT: api/QueueHeader/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QueueHeader/5
        public void Delete(int id)
        {
        }
    }
}
