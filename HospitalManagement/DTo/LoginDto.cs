using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.DTo
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
