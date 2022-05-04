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
    public class Booking : ISupportInitialize
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("SocietyId")]
        public string SocietyId { get; set; }
        
        [BsonElement("Location")]
        public string Location { get; set; }
        
        [BsonElement("RoomId")]
        public int RoomId { get; set; }
        
        [BsonElement("Timespan")]
        public string Timespan { get; set; }

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
