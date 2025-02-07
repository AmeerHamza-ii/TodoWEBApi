using System;
using System.Collections.Generic;

namespace ToDoApplication.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
}
