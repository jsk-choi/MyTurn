using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;
using MyTurn.Service.Interface;

namespace MyTurn.Service
{
    class PersonService : IPerson
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
                    ctx.Person.Add(person);
                    await ctx.SaveChangesAsync();
                    return person;
                }
            }
        }
    }
}
