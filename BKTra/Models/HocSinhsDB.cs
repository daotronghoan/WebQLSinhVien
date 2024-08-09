using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BKTra.Models
{
    public partial class HocSinhsDB : DbContext
    {
        public HocSinhsDB()
            : base("name=HocSinhsDB")
        {
        }

        public virtual DbSet<HocSinh> HocSinhs { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
