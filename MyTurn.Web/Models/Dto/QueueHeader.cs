using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTurn.Web.Models
{
    public class QueueHeader
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public int VendorId { get; set; }
        public string QueueName { get; set; }
        public string QueueDesc { get; set; }
    }
}