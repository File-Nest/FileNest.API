using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace FileNest.Data.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }
    }
}
