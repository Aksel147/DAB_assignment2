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
    public class Member : ISupportInitialize
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("SocietyIds")]
        public List<string> SocietyIds { get; set; }

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
