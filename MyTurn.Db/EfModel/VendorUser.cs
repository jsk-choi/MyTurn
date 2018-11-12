namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorUser")]
    public partial class VendorUser
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public int VendorId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public virtual C_Users C_Users { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
