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
    public class PersonController : ApiController
    {
        private readonly IPersonService PersonService;

        public PersonController(IPersonService _personService) {
            PersonService = _personService;
        }

        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public async Task<IHttpActionResult> Get(int id)

        {
            var person = await PersonService.Get(id);
            var personDto = Mapper.Map<dto.Person>(person);
            return Ok(personDto);
        }

        // POST: api/Test
        public async Task<IHttpActionResult> Post([FromBody]dto.Person person)
        {
            var personEf = Mapper.Map<Person>(person);
            var personNew = await PersonService.AddUpdate(personEf);
            var personDto = Mapper.Map<dto.Person>(personNew);
            return Ok(personDto);
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
