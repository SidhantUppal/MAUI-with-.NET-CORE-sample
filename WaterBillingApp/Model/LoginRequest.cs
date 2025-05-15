using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBillingApp.Model
{
    public class LoginRequest
    {
        [Required] public string Identifier { get; set; } // email or mobile
        [Required] public string Password { get; set; }
        [Required] public string Role { get; set; }
        public string? Division { get; set; }
    }

}
