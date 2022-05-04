using System.ComponentModel;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_assignment2.Models
{
    public class Society : ISupportInitialize
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("CVR")]
        public string CVR { get; set; }
        
        [BsonElement("Activity")]
        public string Activity { get; set; }
        
        [BsonElement("Address")]
        public string Address { get; set; }
        
        [BsonElement("Chairman")]
        public Chairman Chairman { get; set; }
        
        [BsonElement("KeyResponsible")]
        public KeyResponsible KeyResponsible { get; set; }
        
        [BsonExtraElements]
        public IDictionary<string, object> ExtraElements { get; set; }

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
