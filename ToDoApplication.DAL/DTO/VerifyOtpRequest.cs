using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.DAL.DTO
{
    public class VerifyOtpRequest
    {
        public int UserId { get; set; }
        public string OtpCode { get; set; }
    }
}
