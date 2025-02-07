using System;
using System.Collections.Generic;

namespace ToDoApplication.DAL.Models;

public partial class UserActivity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? ActivityDescription { get; set; }

    public DateTime? ActivityTime { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
