using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public interface IPersonService
    {
        Person Get(string Id);
        Task<Person> AddUpdate(Person person);
    }
}
