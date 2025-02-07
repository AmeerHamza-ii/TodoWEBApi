using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.DAL.DTO;
using ToDoTasks.BLL.Interfaces;

namespace ToDoApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOtpService _optService;

        public UserController(IUserService userService, IOtpService optService)
        {
            _userService = userService;
            _optService = optService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var response = await _userService.CreateUserAsync(createUserRequest);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ToDoApplication.DAL.DTO.LoginRequest loginRequest)
        {
            var user = await _userService.ValidateUserAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var otpResponse = await _optService.GenerateOtpForUserAsync(user.Id);
            return Ok(new { message = "OTP sent to email", otpId = otpResponse.OtpId });
        }

        [HttpPost("verifyOtp/{userId}")]
        public async Task<IActionResult> VerifyOtp(int userId, [FromBody] OtpVerificationRequest otpRequest)
        {
            var isVerified = await _userService.VerifyOtpAsync(userId, otpRequest);

            if (!isVerified)
                return BadRequest("Invalid or expired OTP");

            return Ok("OTP Verified successfully");
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest resendOtpRequest)
        {
            var otpResponse = await _optService.ResendOtpAsync(resendOtpRequest.UserId);
            return Ok(new { message = "OTP resent to email", otpId = otpResponse.OtpId });
        }
    }
}
