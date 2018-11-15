using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public class PersonService : IPersonService
    {
        public async Task<Person> AddUpdate(Person person)
        {
            using (var ctx = new MyTurnDb()) {

                var thisPerson = ctx.Person
                    .Where(x => x.TelSms == person.TelSms)
                    .SingleOrDefault();

                if (thisPerson == null)
                {
                    person.CreateDate = DateTime.Now;
                    ctx.Person.Add(person);
                    var task = await ctx.SaveChangesAsync();
                    return person;
                }
                else
                {
                    thisPerson = person;
                    var task = await ctx.SaveChangesAsync();
                    return thisPerson;
                }
            }
        }

        public async Task<Person> Get(int Id)
        {
            using (var ctx = new MyTurnDb())
            {
                var jjj = await ctx.Person.FindAsync(Id);
                return jjj;
            }
        }
    }
}
