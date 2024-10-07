using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>,.<])(?!.*\\s).*$", ErrorMessage = "Password must be 6-10 characters long, with at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")]
        public string Password { get; set; }

    }
}