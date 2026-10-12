using FileNest.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FileNest.Data.Entities
{
    public class User : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string? ClerkUserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [BsonRepresentation(BsonType.String)]
        public UserStatus Status { get; set; } = UserStatus.Active;
        public bool EmailVerified { get; set; }
        public DateTime? LastSeenAt { get; set; }
    }
}
