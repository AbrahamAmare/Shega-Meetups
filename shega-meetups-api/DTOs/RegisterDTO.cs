using System.ComponentModel.DataAnnotations;

namespace shega_meetups_api.DTOs
{

    // Note: The DTO IS A GOOD PLACE TO PERFORM VALIDATION
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}