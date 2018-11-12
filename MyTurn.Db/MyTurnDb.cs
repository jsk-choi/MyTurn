namespace MyTurn.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyTurnDb : DbContext
    {
        public MyTurnDb()
            : base("name=MyTurnDb")
        {
        }

        public virtual DbSet<C_ChangeLog> C_ChangeLog { get; set; }
        public virtual DbSet<C_Role> C_Role { get; set; }
        public virtual DbSet<C_UserClaim> C_UserClaim { get; set; }
        public virtual DbSet<C_UserLogin> C_UserLogin { get; set; }
        public virtual DbSet<C_Users> C_Users { get; set; }
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<QueueDetail> QueueDetail { get; set; }
        public virtual DbSet<QueueHeader> QueueHeader { get; set; }
        public virtual DbSet<QueueStatus> QueueStatus { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorUser> VendorUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_ChangeLog>()
                .Property(e => e.SourceTable)
                .IsUnicode(false);

            modelBuilder.Entity<C_Role>()
                .HasMany(e => e.C_Users)
                .WithMany(e => e.C_Role)
                .Map(m => m.ToTable("_UserRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<C_Users>()
                .HasMany(e => e.C_UserClaim)
                .WithRequired(e => e.C_Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<C_Users>()
                .HasMany(e => e.C_UserLogin)
                .WithRequired(e => e.C_Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<C_Users>()
                .HasMany(e => e.VendorUser)
                .WithRequired(e => e.C_Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.ItemSource)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.ItemDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.AssetName)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.AssetType)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.QueueDetail)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueueHeader>()
                .HasMany(e => e.QueueDetail)
                .WithRequired(e => e.QueueHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueueStatus>()
                .HasMany(e => e.QueueDetail)
                .WithRequired(e => e.QueueStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendor>()
                .HasMany(e => e.VendorUser)
                .WithRequired(e => e.Vendor)
                .WillCascadeOnDelete(false);
        }
    }
}
