using System.ComponentModel.DataAnnotations;

namespace TalabatApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; } 
        
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

    }
}
