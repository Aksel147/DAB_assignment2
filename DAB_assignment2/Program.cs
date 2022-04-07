using DAB_assignment2.Data;
using DAB_assignment2.Models;
using Microsoft.Data.SqlClient;

using (MunicipalityDbContext context = new MunicipalityDbContext())
{
    Console.WriteLine("Seeding...");
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
                Console.WriteLine("{0,40}{1,10}\n",
                    "Location",
                    "Room");
                List<Location> locations = context.Locations.ToList();
                locations.ForEach(l =>
                {
                    Console.WriteLine("{0,40}", l.Address + ":");
                    context.Rooms.Where(r => r.LocationId == l.Address).ToList().ForEach(r =>
                    {
                        Console.WriteLine("{0,50}", r.Id);
                    });
                });
                break;

            case 's':
                Console.Clear();
                // Get all societies
                Console.WriteLine("{0,20}{1,80}",
                    "Society",
                    "Chairman");
                Console.WriteLine("{0,20}{1,20}{2,40}{3,20}{4,20}{5,40}\n",
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
                    Console.WriteLine("{0,20}{1,20}{2,40}{3,20}{4,20}{5,40}",
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
                Console.WriteLine("{0,40}{1,30}{2,20}{3,70}",
                    "Room",
                    "Society",
                    "Chairman",
                    "Timespan");
                Console.WriteLine("{0,40}{1,10}{2,20}{3,20}{4,20}{5,40}{6,10}\n",
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
                    context.Bookings.Where(b => b.RoomId == r.Id).ToList().ForEach(b =>
                    {
                        Society s = context.Societies.FirstOrDefault(s => s.CVR == b.SocietyId);
                        Chairman c = context.Chairmen.FirstOrDefault(c => c.CPR == s.ChairmanId);
                        Console.WriteLine("{0,40}{1,10}{2,20}{3,20}{4,20}{5,40}{6,10}",
                            r.LocationId,
                            r.Id,
                            b.SocietyId,
                            c.CPR,
                            c.Name,
                            c.Address,
                            b.TimespanId);
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
        PeopleLimit = 100
    };
    Location l2 = new Location
    {
        Address = "Gøteborg Allé 7, 8200 Aarhus N",
        Properties = "Basketball, Coffeemachine, Iceskating",
        PeopleLimit = 15
    };
    Location l3 = new Location
    {
        Address = "Helsingforsgade 2, 8200 Aarhus N",
        Properties = "Elevator, Table Tennis, Books, TV, Locker",
        PeopleLimit = 150
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
        Id = 1,
        LocationId = l1.Address,
        PeopleLimit = 25
    };
    Room r2 = new Room
    {
        Id = 2,
        LocationId = l3.Address,
        PeopleLimit = 10
    };
    Room r3 = new Room
    {
        Id = 3,
        LocationId = l3.Address,
        PeopleLimit = 10
    };
    List<Room> rooms = new List<Room>() {r1, r2, r3};
    try
    {
        context.Rooms.BulkInsert(rooms, options => options.InsertKeepIdentity = true);
    }
    catch (SqlException e)
    {
    }
    
    //LocationTimespans
    LocationTimespan lt1 = new LocationTimespan()
    {
        LocationId = l1.Address,
        TimespanId = t1.Span
    };
    LocationTimespan lt2 = new LocationTimespan()
    {
        LocationId = l1.Address,
        TimespanId = t2.Span
    };
    LocationTimespan lt3 = new LocationTimespan()
    {
        LocationId = l1.Address,
        TimespanId = t3.Span
    };
    LocationTimespan lt4 = new LocationTimespan()
    {
        LocationId = l1.Address,
        TimespanId = t4.Span
    };
    LocationTimespan lt5 = new LocationTimespan()
    {
        LocationId = l2.Address,
        TimespanId = t1.Span
    };
    LocationTimespan lt6 = new LocationTimespan()
    {
        LocationId = l2.Address,
        TimespanId = t2.Span
    };
    LocationTimespan lt7 = new LocationTimespan()
    {
        LocationId = l2.Address,
        TimespanId = t3.Span
    };
    LocationTimespan lt8 = new LocationTimespan()
    {
        LocationId = l2.Address,
        TimespanId = t4.Span
    };
    LocationTimespan lt9 = new LocationTimespan()
    {
        LocationId = l3.Address,
        TimespanId = t1.Span
    };
    LocationTimespan lt10 = new LocationTimespan()
    {
        LocationId = l3.Address,
        TimespanId = t2.Span
    };
    List<LocationTimespan> locationTimespans = new List<LocationTimespan>()
        {lt1, lt2, lt3, lt4, lt5, lt6, lt7, lt8, lt9, lt10};
    try
    {
        context.LocationTimespans.BulkInsert(locationTimespans);
    }
    catch (SqlException e)
    {
    }
    
    //RoomTimespans
    RoomTimespan rt1 = new RoomTimespan()
    {
        RoomId = r1.Id,
        TimespanId = t1.Span
    };
    RoomTimespan rt2 = new RoomTimespan()
    {
        RoomId = r1.Id,
        TimespanId = t2.Span
    };
    RoomTimespan rt3 = new RoomTimespan()
    {
        RoomId = r1.Id,
        TimespanId = t3.Span
    };
    RoomTimespan rt4 = new RoomTimespan()
    {
        RoomId = r1.Id,
        TimespanId = t4.Span
    };
    RoomTimespan rt5 = new RoomTimespan()
    {
        RoomId = r2.Id,
        TimespanId = t1.Span
    };
    RoomTimespan rt6 = new RoomTimespan()
    {
        RoomId = r2.Id,
        TimespanId = t2.Span
    };
    RoomTimespan rt7 = new RoomTimespan()
    {
        RoomId = r2.Id,
        TimespanId = t3.Span
    };
    RoomTimespan rt8 = new RoomTimespan()
    {
        RoomId = r2.Id,
        TimespanId = t4.Span
    };
    RoomTimespan rt9 = new RoomTimespan()
    {
        RoomId = r3.Id,
        TimespanId = t1.Span
    };
    RoomTimespan rt10 = new RoomTimespan()
    {
        RoomId = r3.Id,
        TimespanId = t2.Span
    };
    List<RoomTimespan> RoomTimespans = new List<RoomTimespan>()
        {rt1, rt2, rt3, rt4, rt5, rt6, rt7, rt8, rt9, rt10};
    try
    {
        context.RoomTimespans.BulkInsert(RoomTimespans);
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
    Member m1 = new Member(){Id = 1};
    Member m2 = new Member(){Id = 2};
    Member m3 = new Member(){Id = 3};
    Member m4 = new Member(){Id = 4};
    List<Member> members = new List<Member>() {m1, m2, m3, m4};
    try
    {
        context.Members.BulkInsert(members, options => options.InsertKeepIdentity = true);
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
        ChairmanId = c1.CPR
    };
    Society s2 = new Society
    {
        CVR = "3874829-0009",
        Activity = "Baking",
        Address = "Baker Street 50, 8000 Aarhus C",
        ChairmanId = c2.CPR
    };
    Society s3 = new Society
    {
        CVR = "8923470-9877",
        Activity = "Figureskating",
        Address = "Skate Street 110, 8000 Aarhus C",
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
    
    //MemberSocieties
    MemberSociety ms1 = new MemberSociety()
    {
        MemberId = m1.Id,
        SocietyId = s1.CVR
    };
    MemberSociety ms2 = new MemberSociety()
    {
        MemberId = m2.Id,
        SocietyId = s1.CVR
    };
    MemberSociety ms3 = new MemberSociety()
    {
        MemberId = m3.Id,
        SocietyId = s1.CVR
    };
    MemberSociety ms4 = new MemberSociety()
    {
        MemberId = m4.Id,
        SocietyId = s1.CVR
    };
    MemberSociety ms5 = new MemberSociety()
    {
        MemberId = m1.Id,
        SocietyId = s2.CVR
    };
    MemberSociety ms6 = new MemberSociety()
    {
        MemberId = m1.Id,
        SocietyId = s3.CVR
    };
    List<MemberSociety> memberSocieties = new List<MemberSociety>() {ms1, ms2, ms3, ms4, ms5, ms6};
    try
    {
        context.MemberSocieties.BulkInsert(memberSocieties);
    }
    catch (SqlException e)
    {
    }

    //Bookings
    Booking b1 = new Booking
    {
        Id = 1,
        SocietyId = s1.CVR,
        LocationId = l2.Address,
        TimespanId = t1.Span
    };
    Booking b2 = new Booking
    {
        Id = 2,
        SocietyId = s1.CVR,
        LocationId = l2.Address,
        TimespanId = t2.Span
    };
    Booking b3 = new Booking
    {
        Id = 3,
        SocietyId = s1.CVR,
        LocationId = l2.Address,
        TimespanId = t3.Span
    };
    Booking b4 = new Booking
    {
        Id = 4,
        SocietyId = s1.CVR,
        LocationId = l2.Address,
        TimespanId = t4.Span
    };
    Booking b5 = new Booking
    {
        Id = 5,
        SocietyId = s2.CVR,
        LocationId = r3.LocationId,
        RoomId = r3.Id,
        TimespanId = t1.Span
    };
    Booking b6 = new Booking
    {
        Id = 6,
        SocietyId = s2.CVR,
        LocationId = r3.LocationId,
        RoomId = r3.Id,
        TimespanId = t2.Span
    };
    Booking b7 = new Booking
    {
        Id = 7,
        SocietyId = s3.CVR,
        LocationId = r1.LocationId,
        RoomId = r1.Id,
        TimespanId = t1.Span
    };
    Booking b8 = new Booking
    {
        Id = 8,
        SocietyId = s3.CVR,
        LocationId = r1.LocationId,
        RoomId = r1.Id,
        TimespanId = t4.Span
    };
    List<Booking> bookings = new List<Booking>() {b1, b2, b3, b4, b5, b6, b7, b8};
    try
    {
        context.Bookings.BulkInsert(bookings, options => options.InsertKeepIdentity = true);
    }
    catch (SqlException e)
    {
    }
}