using System.Text.Json;

namespace DAB_assignment2.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int PeopleLimit { get; set; }
        public List<string> Availability { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, (new JsonSerializerOptions() {WriteIndented = true}));
        }
    }
}
