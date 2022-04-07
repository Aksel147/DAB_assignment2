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
        public DbSet<Timespan>? Timespans { get; set; }
        public DbSet<LocationTimespan>? LocationTimespans { get; set; }
        public DbSet<RoomTimespan>? RoomTimespans { get; set; }
        public DbSet<MemberSociety>? MemberSocieties { get; set; }
        public DbSet<Access>? Accesses { get; set; }
        public DbSet<KeyResponsible>? KeyResponsibles { get; set; }

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

            modelBuilder.Entity<Timespan>()
                .HasKey(t => t.Span);
            
            modelBuilder.Entity<MemberSociety>()
                .HasKey(ms => new {ms.MemberId, ms.SocietyId});
            
            modelBuilder.Entity<LocationTimespan>()
                .HasKey(lt => new {lt.LocationId, lt.TimespanId});
            
            modelBuilder.Entity<RoomTimespan>()
                .HasKey(rt => new {rt.RoomId, rt.TimespanId});
            
            modelBuilder.Entity<Access>()
                .HasKey(a => a.LocationId);
            
            modelBuilder.Entity<KeyResponsible>()
                .HasKey(kr => kr.SocietyId);
            
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
                .HasForeignKey(b => b.TimespanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Chairman>()
                .HasMany<Society>(c => c.Societies)
                .WithOne(s => s.Chairman)
                .HasForeignKey(s => s.ChairmanId);

            modelBuilder.Entity<Location>()
                .HasMany<Room>(l => l.Rooms)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MemberSociety>()
                .HasOne<Member>(ms => ms.Member)
                .WithMany(m => m.Societies)
                .HasForeignKey(ms => ms.MemberId);
            modelBuilder.Entity<MemberSociety>()
                .HasOne<Society>(ms => ms.Society)
                .WithMany(s => s.Members)
                .HasForeignKey(ms => ms.SocietyId);
            
            modelBuilder.Entity<LocationTimespan>()
                .HasOne<Location>(lt => lt.Location)
                .WithMany(l => l.Availability)
                .HasForeignKey(lt => lt.LocationId);
            modelBuilder.Entity<LocationTimespan>()
                .HasOne<Timespan>(lt => lt.Timespan)
                .WithMany(t => t.Locations)
                .HasForeignKey(lt => lt.TimespanId);
            
            modelBuilder.Entity<RoomTimespan>()
                .HasOne<Room>(rt => rt.Room)
                .WithMany(r => r.Availability)
                .HasForeignKey(rt => rt.RoomId);
            modelBuilder.Entity<RoomTimespan>()
                .HasOne<Timespan>(rt => rt.Timespan)
                .WithMany(r => r.Rooms)
                .HasForeignKey(rt => rt.TimespanId);

            modelBuilder.Entity<Member>()
                .HasOne<Chairman>(l => l.Chairman)
                .WithOne(a => a.Member)
                .HasForeignKey<Chairman>(a => a.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Location>()
                .HasOne<Access>(l => l.Access)
                .WithOne(a => a.Location)
                .HasForeignKey<Access>(a => a.LocationId);
            
            modelBuilder.Entity<Society>()
                .HasOne<KeyResponsible>(l => l.KeyResponsible)
                .WithOne(a => a.Society)
                .HasForeignKey<KeyResponsible>(a => a.SocietyId);
            
            modelBuilder.Entity<Member>()
                .HasOne<KeyResponsible>(l => l.KeyResponsible)
                .WithOne(a => a.Member)
                .HasForeignKey<KeyResponsible>(a => a.MemberId);
        }
    }
}