namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueueDetail")]
    public partial class QueueDetail
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public int QueueHeaderId { get; set; }

        public int QueueStatusId { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public virtual QueueHeader QueueHeader { get; set; }

        public virtual QueueStatus QueueStatus { get; set; }
    }
}
