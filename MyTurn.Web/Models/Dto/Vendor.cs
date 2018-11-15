using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTurn.Web.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string VendorDesc { get; set; }
    }
}