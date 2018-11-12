using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service.Interface
{
    interface IPerson
    {
        Task<Person> AddUpdate(Person person);
    }
}
