// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using DAB_assignment2.Data;
using DAB_assignment2.Models;

Console.WriteLine("Hello, World!");

using (MunicipalityDbContext context = new MunicipalityDbContext())
{
    // Get all Rooms
    List<Location> locations = context.Locations.ToList();
    locations.ForEach(l =>
    {
        Console.WriteLine(l.Address);
        context.Rooms.Where(r => r.LocationId == l.Address).ToList().ForEach(r =>
        {
            Console.WriteLine(r.Id);
        });
    });
    
    // Get all societies
    Console.WriteLine("{0,20}{1,30}",
        "Society",
        "Chairman");
    Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}",
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
    
    // Get all booked rooms
    Console.WriteLine("{0,15}{1,35}{2,20}",
        "Room",
        "Society    +    Chairman",
        "Timespan");
    Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}",
        "Location",
        "ID",
        "CVR",
        "CPR",
        "Name",
        "Address",
        "Span");
    List<Room> bookedRooms = context.Rooms.Where(r => r.Bookings.Count > 0).OrderBy(r => r.LocationId).ToList();
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
}
