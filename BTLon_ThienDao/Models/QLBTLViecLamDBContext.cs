using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;

namespace BTLon_ThienDao.Models
{
    public class QLBTLViecLamDBContext : DbContext
    {
        public QLBTLViecLamDBContext(DbContextOptions<QLBTLViecLamDBContext> options) : base(options) { }
        public DbSet<BaiDang> BaiDangs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiDang>().ToTable("BaiDang");
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
