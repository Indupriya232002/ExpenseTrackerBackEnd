using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class PasswordResetRequest
    {
        public string? email { get; set; }

    }

    public class PasswordResetConfirmation
    {
        public string email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get;set; }
    }

    public class OtpVerificationRequest
    {
        public string EnteredOtp { get; set; }
    }
}
