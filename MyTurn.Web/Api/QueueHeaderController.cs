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
    public class QueueHeaderController : ApiController
    {
        private readonly IQueueHeaderService QueueHeaderService;

        public QueueHeaderController(IQueueHeaderService _queueHeaderService) {
            QueueHeaderService = _queueHeaderService;
        }

        // GET: api/QueueHeader
        public async Task<IHttpActionResult> Get()
        {
            var queueHeaders = await QueueHeaderService.Get();
            var queueHeadersDto = Mapper.Map<IList<dto.QueueHeader>>(queueHeaders);
            return Ok(queueHeadersDto);
        }

        // GET: api/QueueHeader/5
        public async Task<IHttpActionResult> Get(int VendorId)
        {
            var queueHeaders = await QueueHeaderService.Get(VendorId);
            var queueHeadersDto = Mapper.Map<IList<dto.QueueHeader>>(queueHeaders);
            return Ok(queueHeadersDto);
        }

        // POST: api/QueueHeader
        public async Task<IHttpActionResult> Post([FromBody]dto.QueueHeader queueHeader)
        {
            var queueHeaderEf = Mapper.Map<QueueHeader>(queueHeader);
            var queueHeaderNew = await QueueHeaderService.AddUpdate(queueHeaderEf);
            var queueHeaderDto = Mapper.Map<dto.QueueHeader>(queueHeaderNew);
            return Ok(queueHeaderDto);
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
