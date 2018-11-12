namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_ChangeLog")]
    public partial class C_ChangeLog
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(100)]
        public string SourceTable { get; set; }

        public int SourceRecordId { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string SourceRecord { get; set; }
    }
}
