using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAB_assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace DAB_assignment2.Data
{
    public partial class MunicipalityDbContext : DbContext
    {
        public MunicipalityDbContext()
        {
        }

        public MunicipalityDbContext(DbContextOptions<MunicipalityDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("" +
                                        "Server=tcp:prj4server.database.windows.net,1433;" +
                                        "Initial Catalog=DAB_2;" +
                                        "Persist Security Info=False;" +
                                        "User ID=superadmin;" +
                                        "Password=Superpassword1;" +
                                        "MultipleActiveResultSets=False;" +
                                        "Encrypt=True;" +
                                        "TrustServerCertificate=False;" +
                                        "Connection Timeout=30;"
            );
        }

        public DbSet<Booking>? Bookings { get; set; }     
        public DbSet<Chairman>? Chairmen { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Society>? Societies { get; set; }
        public DbSet<Availability>? Availabilities { get; set; }
        public DbSet<Timespan>? Timespans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keys
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Chairman>()
                .HasKey(c => c.CPR);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.Address);

            modelBuilder.Entity<Member>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Room>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Society>()
                .HasKey(s => s.CVR);

            modelBuilder.Entity<Availability>()
                .HasKey(a => a.AvailabilityId);

            modelBuilder.Entity<Timespan>()
                .HasKey(t => t.Span);


            // Relations
            modelBuilder.Entity<Booking>()
                .HasOne<Society>(b => b.Society)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SocietyId);

            modelBuilder.Entity<Booking>()
                .HasOne<Location>(b => b.Location)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.LocationId);

            modelBuilder.Entity<Booking>()
                .HasOne<Room>(b => b.Room)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<Booking>()
                .HasOne<Timespan>(b => b.Timespan)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TimespanId);

            modelBuilder.Entity<Chairman>()
                .HasMany<Society>(c => c.Societies)
                .WithOne(s => s.Chairman)
                .HasForeignKey(s => s.ChairmanId);

            modelBuilder.Entity<Location>()
                .HasMany<Room>(l => l.Rooms)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Availability>()
                .HasMany<Timespan>(a => a.Timespans)
                .WithOne(t => t.Availability)
                .HasForeignKey(t => t.AvailabilityId);

            modelBuilder.Entity<Availability>()
                .HasMany<Room>(a => a.Rooms)
                .WithOne(r => r.Availability)
                .HasForeignKey(r => r.AvailabilityId);

            modelBuilder.Entity<Availability>()
                .HasMany<Location>(a => a.Locations)
                .WithOne(l => l.Availability)
                .HasForeignKey(l => l.AvailabilityId);

            modelBuilder.Entity<Member>()
                .HasMany<Society>(m => m.Societies)
                .WithMany(s => s.Members);


        }
    }
}