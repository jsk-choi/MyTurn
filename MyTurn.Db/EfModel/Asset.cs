namespace MyTurn.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Asset")]
    public partial class Asset
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public int ItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemSource { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemDesc { get; set; }

        [Required]
        [StringLength(100)]
        public string AssetName { get; set; }

        [Required]
        [StringLength(100)]
        public string AssetType { get; set; }
    }
}
