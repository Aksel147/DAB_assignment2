using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_assignment2.Models
{
    public class Location : ISupportInitialize
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Address")]
        public string Address { get; set; }
        
        [BsonElement("Properties")]
        public string Properties { get; set; }
        
        [BsonElement("PeopleLimit")]
        public int PeopleLimit { get; set; }
        
        [BsonElement("Rooms")]
        public List<Room> Rooms { get; set; }
        
        [BsonElement("Availability")]
        public List<string> Availability { get; set; }
        
        [BsonElement("Access")]
        public Access Access { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, (new JsonSerializerOptions() {WriteIndented = true}));
        }
        
        public void BeginInit()
        {
            
        }

        public void EndInit()
        {
            
        }
    }
}
