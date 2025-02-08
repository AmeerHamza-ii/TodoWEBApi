using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Core.DTOs;

//using ToDoApplication.Core.DTOs;
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
            try
            {
                var response = await _userService.CreateUserAsync(createUserRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ToDoApplication.DAL.DTO.LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.ValidateUserAsync(loginRequest.Email, loginRequest.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                var otpResponse = await _optService.GenerateOtpForUserAsync(user.Id);
                return Ok(new { message = "OTP sent to email", otpId = otpResponse.OtpId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost("verifyOtp/{userId}")]
        public async Task<IActionResult> VerifyOtp(int userId, [FromBody] OtpVerificationRequest otpRequest)
        {
            try
            {
                var isVerified = await _userService.VerifyOtpAsync(userId, otpRequest);
                if (!isVerified)
                    return BadRequest("Invalid or expired OTP");
                return Ok("OTP Verified successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest resendOtpRequest)
        {
            try
            {
                var otpResponse = await _optService.ResendOtpAsync(resendOtpRequest.UserId);
                return Ok(new { message = "OTP resent to email", otpId = otpResponse.OtpId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }


        
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userId, request);
                if (updatedUser == null)
                    return NotFound(new { message = "User not found" });

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> SoftDeleteUser(int userId)
        {
            try
            {
                var result = await _userService.SoftDeleteUserAsync(userId);
                if (!result)
                    return NotFound(new { message = "User not found" });

                return Ok(new { message = "User soft deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetUsers([FromBody] UserListRequest request)
        {
            try
            {
                var users = await _userService.GetUsersAsync(request.PageNumber, request.PageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

    }
}
