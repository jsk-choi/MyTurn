using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public interface IVendorService
    {
        Task<Vendor> AddUpdate(Vendor vendor);
        Task<IList<Vendor>> Get();
        Task<Vendor> Get(int Id);
    }
}
