namespace ToDoApplication.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using System.Net.Mail;
    using ToDoApplication.DAL.DTO;
    using ToDoApplication.DAL.Models;
    using ToDoApplication.DAL.Repository.Interfaces;
    using ToDoTasks.BLL.Interfaces;

    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public OtpService(IOtpRepository otpRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Generate OTP
        public async Task<OtpResponse> GenerateOtpForUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var otpCode = new Random().Next(100000, 999999).ToString(); // Generate OTP
            var expiresAt = DateTime.Now.AddMinutes(5); // OTP expiry time (5 minutes)

            var otp = new Otp
            {
                UserId = userId,
                OtpCode = otpCode,
                ExpiresAt = expiresAt
            };

            // Save OTP to database
            await _otpRepository.AddAsync(otp);

            // Send OTP to user's email
            //await SendOtpEmailAsync(user.Email, otpCode);

            return new OtpResponse { OtpId = otp.Id };
        }

        // Verify OTP
        public async Task<bool> VerifyOtpAsync(int userId, string otpCode)
        {
            var otp = await _otpRepository.GetActiveOtpByUserIdAsync(userId);
            if (otp == null || otp.OtpCode != otpCode || otp.ExpiresAt < DateTime.Now)
            {
                return false; // Invalid or expired OTP
            }

            otp.IsUsed = true;
            await _otpRepository.UpdateAsync(otp); // Mark OTP as used

            return true;
        }

        // Resend OTP
        public async Task<OtpResponse> ResendOtpAsync(int userId)
        {
            var otp = await _otpRepository.GetActiveOtpByUserIdAsync(userId);
            if (otp != null && otp.ExpiresAt > DateTime.Now)
            {
                // Resend the existing OTP if it's still valid
                await SendOtpEmailAsync(otp.User.Email, otp.OtpCode);
                return new OtpResponse { OtpId = otp.Id };
            }
            else
            {
                return await GenerateOtpForUserAsync(userId); // Generate and send new OTP
            }
        }

        // Send OTP Email
        private async  System.Threading.Tasks.Task SendOtpEmailAsync(string email, string otpCode)
        {
            var smtpClient = new SmtpClient("smtp.your-email-provider.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@example.com", "your-password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = "Your OTP Code",
                Body = $"Your OTP code is: {otpCode}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
