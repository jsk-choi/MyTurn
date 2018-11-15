using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTurn.Db;

namespace MyTurn.Service
{
    public class VendorService : IVendorService
    {
        public async Task<Vendor> AddUpdate(Vendor vendor)
        {
            using (var ctx = new MyTurnDb()) {

                var thisVendor = await ctx.Vendor
                    .Where(x => x.VendorDesc == vendor.VendorDesc || x.Id == vendor.Id)
                    .SingleOrDefaultAsync();

                if (thisVendor == null)
                {
                    vendor.CreateDate = DateTime.Now;
                    ctx.Vendor.Add(vendor);
                    var task = await ctx.SaveChangesAsync();
                    return vendor;
                }
                else
                {
                    thisVendor.Active = vendor.Active;
                    thisVendor.VendorDesc = vendor.VendorDesc;
                    var task = await ctx.SaveChangesAsync();
                    return thisVendor;
                }
            }
        }

        public async Task<IList<Vendor>> Get()
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.Vendor.Where(x => x.Active).ToListAsync();
            }
        }

        public async Task<Vendor> Get(int Id)
        {
            using (var ctx = new MyTurnDb())
            {
                return await ctx.Vendor.FindAsync(Id);
            }
        }
    }
}
