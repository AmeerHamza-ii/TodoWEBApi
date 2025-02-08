using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.DAL.Models;

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public  ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public  Role Role { get; set; } = null!;

    public  ICollection<Task> Tasks { get; set; } = new List<Task>();

    public  ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
   
}
