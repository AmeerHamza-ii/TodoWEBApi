using System;
using System.Collections.Generic;

namespace ToDoApplication.DAL.Models;

public partial class Otp
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string OtpCode { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
