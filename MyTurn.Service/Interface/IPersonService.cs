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
        Task<Person> AddUpdate(Person person);
        Task<Person> Get(int Id);
    }
}
