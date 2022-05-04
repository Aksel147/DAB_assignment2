namespace DAB_assignment2;

public class ProgramGammel
{
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
    string t1 = new string("08-10");
    string t2 = new string("10-12");
    string t3 = new string("12-14");
    string t4 = new string("14-16");

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
    
    //Members
    Member m1 = new Member(){Id = 1};
    Member m2 = new Member(){Id = 2};
    Member m3 = new Member(){Id = 3};
    Member m4 = new Member(){Id = 4};

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
}
}