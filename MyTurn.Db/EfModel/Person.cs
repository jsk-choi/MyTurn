namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            QueueDetail = new HashSet<QueueDetail>();
        }

        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(100)]
        public string NameFirst { get; set; }

        [StringLength(100)]
        public string NameLast { get; set; }

        [StringLength(100)]
        public string TelSms { get; set; }

        public bool TelConfirmed { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueDetail> QueueDetail { get; set; }
    }
}
