using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.DAL.DTO
{
     public class BaseClass
        {
            public string IP { get; set; }

            [Required(ErrorMessage = "dto-0001")]
            public string DeviceID { get; set; }

            [Required(ErrorMessage = "err-0002")]
            public string OnBoardingChannel { get; set; }
        }
    public class OtpResponse
    {
        public int OtpId { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

}
