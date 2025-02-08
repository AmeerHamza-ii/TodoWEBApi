using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.DAL.Models;

namespace ToDoApplication.DAL.Repository.Interfaces
{
    public interface IOtpRepository
    {
        Task<Otp> GetOtpByUserIdAsync(int userId);
        System.Threading.Tasks.Task CreateOtpAsync(Otp otp);
        System.Threading.Tasks.Task VerifyOtpAsync(Otp otp);
        System.Threading.Tasks.Task AddAsync(Otp otp);
        Task<Otp> GetActiveOtpByUserIdAsync(int userId);
         System.Threading.Tasks.Task UpdateAsync(Otp otp);
        System.Threading.Tasks.Task InvalidatePreviousOtpsAsync(int userId);

    }
}
