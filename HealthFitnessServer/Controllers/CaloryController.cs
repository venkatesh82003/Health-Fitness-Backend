using HealthFitnessServer.DBModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthFitnessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaloryController : ControllerBase
    {
        HealthFitnessContext context = new HealthFitnessContext();
        [HttpGet()]
        public List<Calory> Get()
        {
            return context.Calories.ToList();
        }
        [HttpGet("{date}/{id}")]
        public List<Calory> Get(string date,int id)
        {
            DateOnly parsedDate = DateOnly.Parse(date);
            return context.Calories.Where(x => x.EntryDate == parsedDate&& x.UserId==id).ToList();
        }
        [HttpPost()]
        public Calory Post([FromBody] Calory newCalory)
        {
            Calory calory = new Calory();
            calory.CalorieId = newCalory.CalorieId;
            calory.UserId = newCalory.UserId;
            calory.FoodItem = newCalory.FoodItem;
            calory.Calories = newCalory.Calories;
            calory.EntryDate = newCalory.EntryDate;
            context.Entry(calory).State = EntityState.Added;
            context.Calories.Add(calory);
            context.SaveChanges();
            return calory;
        }
        //[HttpGet("{id}")]
        //public Calory Get(int id)
        //{
        //    return context.Calories.FirstOrDefault(x => x.CalorieId == id);
        //}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Calory workout = context.Calories.ToList().FirstOrDefault(x => x.CalorieId == id);
            context.Entry(workout).State = EntityState.Deleted;
            context.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Calory newCalory)
        {
            Calory calory = context.Calories.FirstOrDefault(x => x.CalorieId == id);
            if (calory != null)
            {
                calory.CalorieId = newCalory.CalorieId;
                calory.UserId = newCalory.UserId;
                calory.FoodItem = newCalory.FoodItem;
                calory.Calories = newCalory.Calories;
                calory.EntryDate = newCalory.EntryDate;
                context.Entry(calory).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
