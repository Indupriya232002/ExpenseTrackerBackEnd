using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? firstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public string? Confirmpassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? phoneNum { get; set; }

        public string? PasswordResetToken { get; set; }
        public DateTime? TokenExpirationTime { get; set; }
    }
}

