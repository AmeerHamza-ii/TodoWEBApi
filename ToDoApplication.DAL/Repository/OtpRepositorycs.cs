using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.DAL.Data;
using ToDoApplication.DAL.Models;
using ToDoApplication.DAL.Repository.Interfaces;

namespace ToDoApplication.DAL.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly StudentDbContext _context;

        public OtpRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<Otp> GetOtpByUserIdAsync(int userId)
        {
            return await _context.Otps.FirstOrDefaultAsync(o => o.UserId == userId && o.IsUsed == false && o.IsActive == true);
        }

        public async System.Threading.Tasks.Task CreateOtpAsync(Otp otp)
        {
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task VerifyOtpAsync(Otp otp)
        {
            otp.IsUsed = true;
            _context.Otps.Update(otp);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Otp otp)
        {
            await _context.Otps.AddAsync(otp);
            await _context.SaveChangesAsync();
        }

        public async Task<Otp> GetActiveOtpByUserIdAsync(int userId)
        {
            return await _context.Otps
                .Where(o => o.UserId == userId && o.IsUsed.HasValue && !o.IsUsed.Value && o.ExpiresAt > DateTime.Now)
                .FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Otp otp)
        {
            _context.Otps.Update(otp);
            await _context.SaveChangesAsync();
        }

    }
}
