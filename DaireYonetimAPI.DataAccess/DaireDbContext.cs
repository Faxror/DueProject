using DaireYönetimAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.DataAccess
{
    public class DaireDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-I45D279;Initial Catalog=DaireDatabaseV5;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Daire>()
             .HasOne(d => d.Bakiye)
             .WithOne(b => b.Daire)
             .HasForeignKey<Bakiye>(b => b.id); // Use DaireId as the foreign key in Bakiye table

            modelBuilder.Entity<Bakiye>()
                .HasOne(b => b.Daire)
                .WithOne(d => d.Bakiye)
                .HasForeignKey<Daire>(d => d.id);
        }
        public DbSet<Bakiye> Bakiyes { get; set; }
        public DbSet<Daire> Daires { get; set; }
    }
}
