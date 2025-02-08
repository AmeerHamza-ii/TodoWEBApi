using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.DAL.Models;

[Table("task")]

public partial class Task
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TaskTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int StatusId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual TaskType TaskType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
