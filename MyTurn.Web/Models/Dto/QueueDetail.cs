using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTurn.Web.Models
{
    public class QueueDetail
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public int QueueHeaderId { get; set; }
        public int QueueStatusId { get; set; }
        public int PersonId { get; set; }
        public decimal Sort { get; set; }
    }
}