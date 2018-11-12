namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QueueStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QueueStatus()
        {
            QueueDetail = new HashSet<QueueDetail>();
        }

        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        [Column("QueueStatus")]
        [StringLength(100)]
        public string QueueStatus1 { get; set; }

        [StringLength(300)]
        public string LongDesc { get; set; }

        public int? SortNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueDetail> QueueDetail { get; set; }
    }
}
