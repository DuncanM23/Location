using LocationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationRepo
{
    public partial class LocationDataContext : DbContext
    {
        public LocationDataContext(DbContextOptions<LocationDataContext> options)
            : base(options)
        {
        }

        //public LocationDataContext(string connectionstring)
        //{
        //    //var optionsBuilder = new DbContextOptionsBuilder<LocationDataContext>();
        //    //optionsBuilder.UseSqlServer
        //}

        public virtual DbSet<Location> Location { get; set; }
        //public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(u => u.Username)
            //        .IsRequired()
            //        .HasMaxLength(50);
            //    entity.Property(u => u.FirstName)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //    entity.Property(u => u.LastName)
            //        .IsRequired()
            //        .HasMaxLength(50);
            //});

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .UseIdentityColumn();

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LocationDate)
                .IsRequired();

            });
        }
    }
}
