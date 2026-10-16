using System.ComponentModel.DataAnnotations;
namespace FileNest.Model.Models
{
    public class CreateUserRequestModel
    {
        [Required(ErrorMessage = "Name is required.")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string Email { get; set; }
    }
}
