using System.Diagnostics;
using System.Text.Json;
using DAB_assignment2.Models;
using DAB_assignment2.Services;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using JsonSerializer = System.Text.Json.JsonSerializer;

string Db = "MunicipalityDb";
string connString = "mongodb://localhost:27017";

MemberService memberService = new MemberService(Db, connString);
SocietyService societyService = new SocietyService(Db, connString);
BookingService bookingService = new BookingService(Db, connString);
LocationService locationService = new LocationService(Db, connString);

Console.WriteLine("Seeding...");
Seed();

for (;;)
{
    Console.Clear();
    Console.WriteLine("-- Municipality database --\n");
    Console.WriteLine("Choose option:\n");
    Console.WriteLine("r: get all rooms");
    Console.WriteLine("s: get all societies");
    Console.WriteLine("b: get all booked rooms");
    Console.WriteLine("k: get all bookings for a key-responsible");

    switch (Console.ReadKey(true).KeyChar)
    {
        case 'r':
            Console.Clear();
            // Get all Rooms
            locationService.Get().ForEach(l =>
            {
                Console.WriteLine(l);
            });
            break;

        case 's':
            Console.Clear();
            // Get all societies
            societyService.Get().OrderBy(s => s.Activity).ToList().ForEach(s =>
            {
                Console.WriteLine(s);
            });
            break;

        case 'b':
            Console.Clear();
            // Get all booked rooms
            bookingService.Get().Where(b => b.RoomId != null).ToList().ForEach(b =>
            {
                Console.WriteLine(b);
            });
            break;
        
        case 'k':
            Console.Clear();
            // Get all booked rooms
            Console.WriteLine("Type in your phone number");
            string number = Console.ReadLine();

            societyService.Get().Where(s => s.KeyResponsible.PhoneNumber == number).ToList().ForEach(s =>
            {
                bookingService.Get().Where(b => b.SocietyId == s.CVR).ToList().ForEach(b =>
                {
                    Console.WriteLine(b);
                });
            });
            break;
    }

    Console.WriteLine("\nPress any key to go back..");
    char c = Console.ReadKey(true).KeyChar;
}

void Seed()
{
    var client = new MongoClient(connString);
    var database = client.GetDatabase(Db);
    database.DropCollection("Bookings");
    database.DropCollection("Locations");
    database.DropCollection("Societies");
    database.DropCollection("Members");
    
    //Data seeding 
    //Timespans
    string t1 = new string("08-10");
    string t2 = new string("10-12");
    string t3 = new string("12-14");
    string t4 = new string("14-16");
    List<string> a1 = new List<string>() {t1, t2, t3, t4};

    //Rooms
    Room r1 = new Room
    {
        PeopleLimit = 25
    };
    Room r2 = new Room
    {
        PeopleLimit = 10
    };
    Room r3 = new Room
    {
        PeopleLimit = 10
    };
    
    //Locations
    Location l1 = new Location
    {
        Address = "Finlandsgade 22, 8200 Aarhus N",
        Properties = "Whiteboard, Coffeemachine",
        PeopleLimit = 100,
        Rooms = new List<Room>(){r1, r2},
        Availability = a1,
        Access = new Access()
        {
            Codes = "1234",
            KeyLocation = "Under stenen"
        }
    };
    Location l2 = new Location
    {
        Address = "Gøteborg Allé 7, 8200 Aarhus N",
        Properties = "Basketball, Coffeemachine, Iceskating",
        PeopleLimit = 15,
        Availability = a1,
        Access = new Access()
        {
            Codes = "1234, 8888",
            KeyLocation = "Over stenen"
        }
    };
    Location l3 = new Location
    {
        Address = "Helsingforsgade 2, 8200 Aarhus N",
        Properties = "Elevator, Table Tennis, Books, TV, Locker",
        PeopleLimit = 150,
        Rooms = new List<Room>(){r3},
        Availability = a1,
        Access = new Access()
        {
            Codes = "1212",
            KeyLocation = "Ved siden af stenen"
        }
    };
    locationService.Create(l1);
    locationService.Create(l2);
    locationService.Create(l3);

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