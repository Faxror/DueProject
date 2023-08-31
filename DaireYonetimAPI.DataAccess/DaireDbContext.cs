using DaireYönetimAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DaireYonetimAPI.DataAccess
{
    public class DaireDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=localhost;Port=5434;Database=DaireYonetimV1;User Id=postgres;Password=Asdfgh7890;");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-I45D279;Initial Catalog=DaireDatabaseV6;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //optionsBuilder.UseSqlServer(connection, b => b.MigrationsAssembly("DaireYonetimAPI"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Daire>()
            // .HasOne(d => d.Bakiye)
            // .WithOne(b => b.Daire)
            // .HasForeignKey<Bakiye>(b => b.id); // Use DaireId as the foreign key in Bakiye table

            //modelBuilder.Entity<Bakiye>()
            //    .HasOne(b => b.Daire)
            //    .WithOne(d => d.Bakiye)
            //    .HasForeignKey<Daire>(d => d.id);
        }
        public DbSet<Bakiye> Bakiyes { get; set; }
        public DbSet<Daire> Daires { get; set; }
        public DbSet<Config> Configs { get; set; }
    }
}
