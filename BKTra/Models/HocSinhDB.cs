using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BKTra.Models
{
    public partial class HocSinhDB : DbContext
    {
        public HocSinhDB()
            : base("name=HocSinhDB")
        {
        }

        public virtual DbSet<HocSinh> HocSinhs { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
