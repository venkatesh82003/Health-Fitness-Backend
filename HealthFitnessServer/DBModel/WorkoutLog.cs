using System;
using System.Collections.Generic;

namespace HealthFitnessServer.DBModel;

public partial class WorkoutLog
{
    public int WorkoutId { get; set; }

    public int UserId { get; set; }

    public string WorkoutType { get; set; } = null!;

    public int Duration { get; set; }

    public int CaloriesBurned { get; set; }

    public DateOnly WorkoutDate { get; set; }

    public virtual User? User { get; set; }
}
