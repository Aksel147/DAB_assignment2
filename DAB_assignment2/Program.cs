using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
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
            locationService.GetRooms().ForEach(l =>
            {
                Console.WriteLine("");
                Console.WriteLine(l.Address + ":");
                l.Rooms.ForEach(r => { Console.WriteLine("    Room: " + r.Id); });
            });
            break;

        case 's':
            Console.Clear();
            // Get all societies
            societyService.GetByActivity().ForEach(s =>
            {
                Console.WriteLine("");
                Console.WriteLine("Activity: " + s.Activity);
                Console.WriteLine("CVR: " + s.CVR);
                Console.WriteLine("Address: " + s.Address);
                Console.WriteLine("Chairman: " + s.Chairman);
            });
            break;

        case 'b':
            Console.Clear();
            // Get all booked rooms
            bookingService.GetRooms().ForEach(b =>
            {
                Console.WriteLine("");
                Console.WriteLine("Address: " + b.Location.Address);
                Console.WriteLine("Room: " + b.Room?.Id);
                Console.WriteLine("Society-CVR: " + b.Society.CVR);
                Console.WriteLine("Society-Chairman: " + b.Society.Chairman);
                Console.WriteLine("Timespan: " + b.Timespan);
            });
            break;

        case 'k':
            Console.Clear();
            // Get all bookings from key-responsible
            Console.WriteLine("Type in your phone number");
            string number = Console.ReadLine()??"";

            bookingService.GetByKeyResponsible(number).ForEach(b =>
            {
                Console.WriteLine("");
                Console.WriteLine("Address: " + b.Location.Address);
                if (b.Room != null)
                {
                    Console.WriteLine("Room: " + b.Room.Id);
                }

                Console.WriteLine("Access: " + b.Location.Access);
                Console.WriteLine("Timespan: " + b.Timespan);
            });
            break;
    }

    Console.WriteLine("\nPress any key to go back..");
    char c = Console.ReadKey(true).KeyChar;
}

void Seed()
{
    //Drop collections
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
        Id = 1,
        PeopleLimit = 25
    };
    Room r2 = new Room
    {
        Id = 2,
        PeopleLimit = 10
    };
    Room r3 = new Room
    {
        Id = 3,
        PeopleLimit = 10
    };

    //Locations
    Location l1 = new Location
    {
        Address = "Finlandsgade 22, 8200 Aarhus N",
        Properties = "Whiteboard, Coffeemachine",
        PeopleLimit = 100,
        Rooms = new List<Room>() {r1, r2},
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
        Rooms = new List<Room>() {r3},
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

    //Societies
    Member m1 = new Member();
    Member m2 = new Member();
    memberService.Create(m1);
    memberService.Create(m2);

    Society s1 = new Society
    {
        CVR = "289347-9849",
        Activity = "PingPong",
        Address = "Stadion Allé 20, 8000 Aarhus C",
        Chairman = new Chairman()
        {
            CPR = "123456-7890",
            Name = "Aksel Chairman",
            Address = "Stadion Allé 30, 8000 Aarhus C",
            MemberId = m1.Id
        },
        KeyResponsible = new KeyResponsible()
        {
            Address = "Hovedvejen 10",
            Identification = "187hj3212",
            MemberId = m1.Id,
            PhoneNumber = "30492811"
        }
    };
    Society s2 = new Society
    {
        CVR = "3874829-0009",
        Activity = "Baking",
        Address = "Baker Street 50, 8000 Aarhus C",
        Chairman = new Chairman
        {
            CPR = "987654-1238",
            Name = "Sissi Chairman",
            Address = "Fredriks Allé 8, 8000 Aarhus C",
            MemberId = m1.Id
        },
        KeyResponsible = new KeyResponsible()
        {
            Address = "Hovedvejen 10",
            Identification = "187hj3212",
            MemberId = m1.Id,
            PhoneNumber = "30492811"
        }
    };
    Society s3 = new Society
    {
        CVR = "8923470-9877",
        Activity = "Figureskating",
        Address = "Skate Street 110, 8000 Aarhus C",
        Chairman = new Chairman
        {
            CPR = "888995-3283",
            Name = "Anton Chairman",
            Address = "Sesame Street 4, 8000 Aarhus C",
            MemberId = m2.Id
        },
        KeyResponsible = new KeyResponsible()
        {
            Address = "Hovedvejen 10",
            Identification = "187hj3212",
            MemberId = m2.Id,
            PhoneNumber = "88888888"
        }
    };
    societyService.Create(s1);
    societyService.Create(s2);
    societyService.Create(s3);

    //Members
    Member m3 = new Member() {SocietyIds = new List<string>() {s1.Id}};
    Member m4 = new Member() {SocietyIds = new List<string>() {s2.Id}};
    memberService.Create(m3);
    memberService.Create(m4);
    memberService.Update(m1.Id, new Member() {Id = m1.Id, SocietyIds = new List<string>() {s1.Id, s2.Id}});
    memberService.Update(m2.Id, new Member() {Id = m2.Id, SocietyIds = new List<string>() {s3.Id}});

    //Bookings
    Booking b1 = new Booking
    {
        Society = s1,
        Location = l1,
        Room = r1,
        Timespan = t1
    };
    Booking b2 = new Booking
    {
        Society = s2,
        Location = l3,
        Room = r3,
        Timespan = t1
    };
    Booking b3 = new Booking
    {
        Society = s3,
        Location = l1,
        Room = r1,
        Timespan = t2
    };
    Booking b4 = new Booking
    {
        Society = s3,
        Location = l2,
        Timespan = t1
    };
    bookingService.Create(b1);
    bookingService.Create(b2);
    bookingService.Create(b3);
    bookingService.Create(b4);
}