using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Interfaces
{
    public interface IUserRepo
    {
        string Login(Login login);
        object SignUp(Users users);

        List<Users> GetAllUsers();
        // object Logout(Users users); 

        Users GetUserByEmail(string email);
        string GeneratePasswordResetToken(string email);
        bool ResetPasswordWithToken(string email, string newPassword,string confirmPassword);
        string GeneratePasswordResetOtp(string email);
    }
}
