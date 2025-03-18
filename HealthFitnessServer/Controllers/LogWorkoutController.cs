using HealthFitnessServer.DBModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthFitnessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogWorkoutController : ControllerBase
    {
        HealthFitnessContext context = new HealthFitnessContext();
        [HttpGet()]
        public List<WorkoutLog> Get()
        {
            return context.WorkoutLogs.ToList();
        }
        [HttpPost()]
        public void Post([FromBody] WorkoutLog newWorkout)
        {
            WorkoutLog workout = new WorkoutLog();
            workout.WorkoutId=newWorkout.WorkoutId;
            workout.UserId=newWorkout.UserId;
            workout.WorkoutType=newWorkout.WorkoutType;
            workout.Duration=newWorkout.Duration;
            workout.CaloriesBurned=newWorkout.CaloriesBurned;
            workout.WorkoutDate=newWorkout.WorkoutDate;
            context.Entry(workout).State = EntityState.Added;
            context.WorkoutLogs.Add(workout);
            context.SaveChanges();
        }
        [HttpGet("{id}")]
        public WorkoutLog Get(int id)
        {
            return context.WorkoutLogs.FirstOrDefault(x => x.WorkoutId == id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            WorkoutLog workout = context.WorkoutLogs.ToList().FirstOrDefault(x => x.WorkoutId == id);
            context.Entry(workout).State = EntityState.Deleted;
            context.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WorkoutLog newWorkout)
        {
            WorkoutLog workout = context.WorkoutLogs.FirstOrDefault(x => x.WorkoutId == id);
            if (workout != null)
            {
                workout.WorkoutId = newWorkout.WorkoutId;
                workout.UserId = newWorkout.UserId;
                workout.WorkoutType = newWorkout.WorkoutType;
                workout.Duration = newWorkout.Duration;
                workout.CaloriesBurned = newWorkout.CaloriesBurned;
                workout.WorkoutDate = newWorkout.WorkoutDate;
                context.Entry(workout).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
