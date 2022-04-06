using DAB_assignment2.Data;
using DAB_assignment2.Models;

using (MunicipalityDbContext context = new MunicipalityDbContext())
{
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
        Console.WriteLine("Press any key to go back..");
        char c = Console.ReadKey(true).KeyChar;
    }
}