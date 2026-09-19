using System.ComponentModel.DataAnnotations;
namespace FileNest.Model.Models
{
    public class CreateUserRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
