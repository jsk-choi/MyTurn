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

                var thisPerson = ctx.Person.Where(x => x.Id == person.Id);

                if (thisPerson.Any())
                {
                    var thisPersonSave = thisPerson.FirstOrDefault();
                    thisPersonSave = person;
                    await ctx.SaveChangesAsync();
                    return thisPersonSave;
                }
                else
                {
                    person.CreateDate = DateTime.Now;
                    ctx.Person.Add(person);
                    await ctx.SaveChangesAsync();
                    return person;
                }
            }
        }

        public Person Get(string Id)
        {
            using (var ctx = new MyTurnDb())
            {
                return ctx.Person.Where(x => x.TelSms == Id).FirstOrDefault();
            }
        }
    }
}
