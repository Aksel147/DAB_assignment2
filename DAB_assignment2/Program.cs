using DAB_assignment2.Data;
using DAB_assignment2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;

using (MunicipalityDbContext context = new MunicipalityDbContext())
{
    Seed(context);

    for (;;)
    {
        Console.Clear();
        Console.WriteLine("-- Municipality database --\n");
        Console.WriteLine("Choose option:\n");
        Console.WriteLine("r: get all rooms");
        Console.WriteLine("s: get all societies");
        Console.WriteLine("b: get all booked rooms");

        switch (Console.ReadKey(true).KeyChar)
        {
            case 'r':
                Console.Clear();
                // Get all Rooms
                Console.WriteLine("{0,0}{1,30}\n",
                    "Location",
                    "Room");
                List<Location> locations = context.Locations.ToList();
                locations.ForEach(l =>
                {
                    Console.WriteLine(l.Address + ":");
                    context.Rooms.Where(r => r.LocationId == l.Address).ToList().ForEach(r =>
                    {
                        Console.WriteLine("{0,30}", r.Id);
                    });
                });
                break;

            case 's':
                Console.Clear();
                // Get all societies
                Console.WriteLine("{0,20}{1,30}",
                    "Society",
                    "Chairman");
                Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}\n",
                    "CVR",
                    "Activity",
                    "Address",
                    "CPR",
                    "Name",
                    "Address");
                List<Society> societies = context.Societies.OrderBy(s => s.Activity).ToList();
                societies.ForEach(s =>
                {
                    Chairman c = context.Chairmen.Where(c => c.CPR == s.ChairmanId).FirstOrDefault();
                    Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}",
                        s.CVR,
                        s.Activity,
                        s.Address,
                        c.CPR,
                        c.Name,
                        c.Address);
                });
                break;

            case 'b':
                Console.Clear();
                // Get all booked rooms
                Console.WriteLine("{0,15}{1,35}{2,20}",
                    "Room",
                    "Society    +    Chairman",
                    "Timespan");
                Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}\n",
                    "Location",
                    "ID",
                    "CVR",
                    "CPR",
                    "Name",
                    "Address",
                    "Span");
                List<Room> bookedRooms =
                    context.Rooms.Where(r => r.Bookings.Count > 0).OrderBy(r => r.LocationId).ToList();
                bookedRooms.ForEach(r =>
                {
                    r.Bookings.ForEach(b =>
                    {
                        Chairman c = context.Chairmen.Where(c => c.Societies.Contains(b.Society)).FirstOrDefault();
                        Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}",
                            r.LocationId,
                            r.Id,
                            b.SocietyId,
                            c.CPR,
                            c.Name,
                            c.Address,
                            b.Timespan.Span);
                    });
                });
                break;
        }

        Console.WriteLine("\nPress any key to go back..");
        char c = Console.ReadKey(true).KeyChar;
    }
}

void Seed(MunicipalityDbContext context)
{
    //Data seeding 
    //Timespans
    Timespan t1 = new Timespan
    {
        Span = "08-10",
    };
    Timespan t2 = new Timespan()
    {
        Span = "10-12",
    };
    Timespan t3 = new Timespan()
    {
        Span = "12-14",
    };
    Timespan t4 = new Timespan()
    {
        Span = "14-16",
    };
    List<Timespan> timespans = new List<Timespan>() {t1, t2, t3, t4};
    try
    {
        context.Timespans.BulkInsert(timespans);
    }
    catch (SqlException e)
    {
    }

    //Locations
    Location l1 = new Location
    {
        Address = "Finlandsgade 22, 8200 Aarhus N",
        Properties = "Whiteboard, Coffeemachine",
        PeopleLimit = 100,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    Location l2 = new Location
    {
        Address = "Gøteborg Allé 7, 8200 Aarhus N",
        Properties = "Basketball, Coffeemachine, Iceskating",
        PeopleLimit = 15,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    Location l3 = new Location
    {
        Address = "Helsingforsgade 2, 8200 Aarhus N",
        Properties = "Elevator, Table Tennis, Books, TV, Locker",
        PeopleLimit = 150,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    List<Location> locations = new List<Location>() {l1, l2, l3};
    try
    {
        context.Locations.BulkInsert(locations);
    }
    catch (SqlException e)
    {
    }

    //Rooms
    Room r1 = new Room
    {
        LocationId = l3.Address,
        PeopleLimit = 25,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    Room r2 = new Room
    {
        LocationId = l3.Address,
        PeopleLimit = 10,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    Room r3 = new Room
    {
        LocationId = l1.Address,
        PeopleLimit = 10,
        Availability = new List<Timespan>()
        {
            t1, t2, t3, t4
        }
    };
    List<Room> rooms = new List<Room>() {r1, r2, r3};
    try
    {
        context.Rooms.BulkInsert(rooms);
    }
    catch (SqlException e)
    {
    }

    //Chairmen
    Chairman c1 = new Chairman
    {
        CPR = "123456-7890",
        Name = "Aksel Chairman",
        Address = "Stadion Allé 30, 8000 Aarhus C",
    };
    Chairman c2 = new Chairman
    {
        CPR = "987654-1238",
        Name = "Sissi Chairman",
        Address = "Fredriks Allé 8, 8000 Aarhus C",
    };
    Chairman c3 = new Chairman
    {
        CPR = "888995-3283",
        Name = "Anton Chairman",
        Address = "Sesame Street 4, 8000 Aarhus C",
    };
    List<Chairman> chairmen = new List<Chairman>() {c1, c2, c3};
    try
    {
        context.Chairmen.BulkInsert(chairmen);
    }
    catch (SqlException e)
    {
    }
    
    //Members
    Member m1 = new Member();
    Member m2 = new Member();
    Member m3 = new Member();
    Member m4 = new Member();
    List<Member> members = new List<Member>() {m1, m2, m3, m4};
    try
    {
        context.Members.BulkInsert(members);
    }
    catch (SqlException e)
    {
    }

    //Societies
    Society s1 = new Society
    {
        CVR = "289347-9849",
        Activity = "PingPong",
        Address = "Stadion Allé 20, 8000 Aarhus C",
        Members = members,
        ChairmanId = c1.CPR
    };
    Society s2 = new Society
    {
        CVR = "3874829-0009",
        Activity = "Baking",
        Address = "Baker Street 50, 8000 Aarhus C",
        Members = members,
        ChairmanId = c2.CPR
    };
    Society s3 = new Society
    {
        CVR = "8923470-9877",
        Activity = "Figureskating",
        Address = "Skate Street 110, 8000 Aarhus C",
        Members = new List<Member> {m1, m2},
        ChairmanId = c3.CPR
    };
    List<Society> societies = new List<Society>() {s1, s2, s3};
    try
    {
        context.Societies.BulkInsert(societies);
    }
    catch (SqlException e)
    {
    }

    // // Bookings
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 1,
    //     SocietyId = s1.CVR,
    //     LocationId = l2.Address,
    //     TimespanId = l2.Availability[0].Span,
    // });
    //
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 2,
    //     SocietyId = s1.CVR,
    //     LocationId = l2.Address,
    //     TimespanId = l2.Availability[1].Span,
    // });
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 3,
    //     SocietyId = s1.CVR,
    //     LocationId = l2.Address,
    //     TimespanId = l2.Availability[2].Span,
    // });
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 4,
    //     SocietyId = s1.CVR,
    //     LocationId = l2.Address,
    //     TimespanId = l2.Availability[3].Span,
    // });
    //
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 5,
    //     SocietyId = s2.CVR,
    //     LocationId = r3.LocationId,
    //     RoomId = r3.Id,
    //     TimespanId = r3.Availability[0].Span,
    // });
    //
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 6,
    //     SocietyId = s2.CVR,
    //     LocationId = r3.LocationId,
    //     RoomId = r3.Id,
    //     TimespanId = r3.Availability[1].Span,
    // });
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 7,
    //     SocietyId = s3.CVR,
    //     LocationId = r1.LocationId,
    //     RoomId = r1.Id,
    //     TimespanId = r1.Availability[1].Span,
    // });
    // modelBuilder.Entity<Booking>().HasData(new Booking
    // {
    //     Id = 8,
    //     SocietyId = s3.CVR,
    //     LocationId = r1.LocationId,
    //     RoomId = r1.Id,
    //     TimespanId = r1.Availability[2].Span,
    // });
}