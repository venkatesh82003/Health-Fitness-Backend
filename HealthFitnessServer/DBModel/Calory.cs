using System;
using System.Collections.Generic;

namespace HealthFitnessServer.DBModel;

public partial class Calory
{
    public int CalorieId { get; set; }

    public int UserId { get; set; }

    public string FoodItem { get; set; } = null!;

    public int Calories { get; set; }

    public DateOnly EntryDate { get; set; }

    public virtual User? User { get; set; }
}
