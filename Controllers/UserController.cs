using BusinessLayer.Services;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            var token = _userService.Login(login);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid Username or Password");
            }
            return Ok(new { token });
        }

        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp(Users users)
        {
            return Ok(new { message = _userService.SignUp(users) });
        }

        [Route("GetUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

        [Route("RequestPasswordReset")]
        [HttpPost]
        public IActionResult RequestPasswordReset([FromBody] PasswordResetRequest request)
        {
            // Check if phone number is provided
            if (string.IsNullOrEmpty(request.email))
            {
                return BadRequest("Email is required");
            }

            // Check if the user exists
            var user = _userService.GetUserByEmail(request.email);
            if (user == null)
            {
                return NotFound("No account found with this email address");
            }

            // Generate OTP for password reset
            var otp = _userService.GeneratePasswordResetOtp(request.email);
            if (string.IsNullOrEmpty(otp))
            {
                return BadRequest("Failed to generate OTP");
            }

            // Send OTP to user's email
            EmailService.Send(user.Email, "Password Reset OTP", $"Your OTP is: {otp}");


            // Return success response
            return Ok(new { otp });
        }

        [Route("ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword([FromBody] PasswordResetConfirmation request)
        {
            if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
                return BadRequest(new { message = "Email and new password are required." });

            var result = _userService.ResetPasswordWithToken(request.email, request.NewPassword,request.ConfirmPassword);
            return result ? Ok(new { message = "Password has been reset successfully." })
                          : BadRequest(new { message = "password reset failed.Please ensure your details are correct." });
        }

    }
}
