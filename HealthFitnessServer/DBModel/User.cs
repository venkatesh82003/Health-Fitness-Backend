using System;
using System.Collections.Generic;

namespace HealthFitnessServer.DBModel;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Calory> Calories { get; set; } = new List<Calory>();

    public virtual ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();
}
