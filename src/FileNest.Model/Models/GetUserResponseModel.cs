using System.ComponentModel.DataAnnotations;

namespace FileNest.Model.Models
{
    public class GetUserResponseModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastSeenAt { get; set; }
    }
}
