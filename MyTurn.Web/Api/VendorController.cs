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
    [Authorize(Roles = "Admin")]
    public class VendorController : ApiController
    {
        private readonly IVendorService VendorService;

        public VendorController(IVendorService _vendorService) {
            VendorService = _vendorService;
        }

        // GET: api/Vendor
        public async Task<IHttpActionResult> Get()
        {
            var vendors = await VendorService.Get();
            var vendorsDto = Mapper.Map<IList<dto.Vendor>>(vendors);
            return Ok(vendorsDto);
        }

        // GET: api/Vendor/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var vendor = await VendorService.Get(id);
            var vendorDto = Mapper.Map<dto.Vendor>(vendor);
            return Ok(vendorDto);
        }

        // POST: api/Vendor
        public async Task<IHttpActionResult> Post([FromBody]dto.Vendor vendor)
        {
            var vendorEf = Mapper.Map<Vendor>(vendor);
            var vendorNew = await VendorService.AddUpdate(vendorEf);
            var vendorDto = Mapper.Map<dto.Vendor>(vendorNew);
            return Ok(vendorDto);
        }

        // PUT: api/Vendor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vendor/5
        public void Delete(int id)
        {
        }
    }
}
