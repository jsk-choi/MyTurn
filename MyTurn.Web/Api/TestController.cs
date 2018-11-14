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

namespace MyTurn.Web.Api
{
    public class TestController : ApiController
    {
        private readonly IPersonService PersonService;

        public TestController(IPersonService _personService) {
            PersonService = _personService;
        }

        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public IHttpActionResult Get(string id)
        {
            var person = PersonService.Get(id);
            var personDto = Mapper.Map<dto.Person>(person);
            return Ok(personDto);
        }

        // POST: api/Test
        public IHttpActionResult Post([FromBody]dto.Person person)
        {
            var personEf = Mapper.Map<Person>(person);
            return Ok(PersonService.AddUpdate(personEf));
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
