using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService
    {
        IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public string Login(Login login)
        {
            return _userRepo.Login(login);
        }

        public object SignUp(Users users)
        {
            return _userRepo.SignUp(users);
        }

        public List<Users> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public Users GetUserByEmail(string email)
        {
            return _userRepo.GetUserByEmail(email);
        }

        public string GeneratePasswordResetToken(string email)
        {
            return _userRepo.GeneratePasswordResetToken(email);
        }

        public bool ResetPasswordWithToken(string email, string newPassword,string confirmPassword)
        {
            return _userRepo.ResetPasswordWithToken(email, newPassword,confirmPassword);
        }

        public string GeneratePasswordResetOtp(string email)
        {
            return _userRepo.GeneratePasswordResetOtp(email);
        }
    }
}
