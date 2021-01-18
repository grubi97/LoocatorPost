using System;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Reading> Readings { get; set; }
        public DbSet<Sensor> Sensors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Reading>()
                    .HasOne(u => u.Sensor)
                    .WithMany(a => a.Readings)
                    .HasForeignKey(u => u.SensorId);



        }
    }


}
