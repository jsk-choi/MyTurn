using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public interface IAssetService
    {
        Task<Asset> AddUpdate(Asset asset);
        Task<Asset> Delete(Asset asset);
        Task<Asset> Get(int Id);
        Task<Asset> Get(int ItemId, string ItemSource);
    }
}
