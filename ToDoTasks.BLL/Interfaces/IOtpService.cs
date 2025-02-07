using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.DAL.DTO;
namespace ToDoTasks.BLL.Interfaces
{
    public interface IOtpService
    {
        Task<OtpResponse> GenerateOtpForUserAsync(int userId);
        Task<bool> VerifyOtpAsync(int userId, string otpCode);
        Task<OtpResponse> ResendOtpAsync(int userId);
    }
}
    