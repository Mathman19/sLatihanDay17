namespace XPos.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class XPosContext : DbContext
    {
        public XPosContext()
            : base("name=XPosContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<MstCategory> MstCategories { get; set; }
        public virtual DbSet<MstOrderDetail> MstOrderDetails { get; set; }
        public virtual DbSet<MstOrderHeader> MstOrderHeaders { get; set; }
        public virtual DbSet<MstProduct> MstProducts { get; set; }
        public virtual DbSet<MstVariant> MstVariants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstCategory>()
                .HasMany(e => e.MstVariants)
                .WithRequired(e => e.MstCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<MstOrderDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MstOrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MstOrderHeader>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MstOrderHeader>()
                .HasMany(e => e.MstOrderDetails)
                .WithRequired(e => e.MstOrderHeader)
                .HasForeignKey(e => e.HeaderId);

            modelBuilder.Entity<MstProduct>()
                .Property(e => e.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MstProduct>()
                .HasMany(e => e.MstOrderDetails)
                .WithRequired(e => e.MstProduct)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<MstVariant>()
                .HasMany(e => e.MstProducts)
                .WithRequired(e => e.MstVariant)
                .HasForeignKey(e => e.VariantId);
        }
    }
}
