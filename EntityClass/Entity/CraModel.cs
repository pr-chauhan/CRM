using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EntityClass
{
    public partial class CraModel : DbContext
    {
        public CraModel()
            : base("name=CraModel")
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Consignee> Consignees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoice_Detail> Invoice_Detail { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User_detail> User_detail { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
               .Property(e => e.PCess_Per)
               .HasPrecision(18, 3);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.VAT_CST_per)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Surcharge_PER)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Insurance_per)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Item>()
                .Property(e => e.Tarriff_no)
                .HasPrecision(18, 4);
        }
    }
}
