using System.Text;
using HealthFitnessServer.DBModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthFitnessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        HealthFitnessContext context = new HealthFitnessContext();
        [HttpGet()]
        public List<User> Get()
        {
            return context.Users.ToList();
        }
        [HttpGet("{email}")]
        public User Get(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email == email);
        }
        [HttpPost()]
        public void Post([FromBody] User newUser)
        {
            string hashedPassword = GenerateSHA256Hash(newUser.PasswordHash);
            User user = new User();
            user.Email = newUser.Email;
            user.FullName = newUser.FullName;
            user.Mobile = newUser.Mobile;
            user.PasswordHash = hashedPassword;
            user.Age = newUser.Age;
            user.Gender = newUser.Gender;
            context.Entry(user).State = EntityState.Added;
            context.Users.Add(user);
            context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = context.Users.ToList().FirstOrDefault(x => x.Id == id);
            context.Entry(user).State = EntityState.Deleted;
            context.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User newUser)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.Email = newUser.Email;
                user.FullName = newUser.FullName;
                user.Mobile = newUser.Mobile;
                user.PasswordHash = newUser.PasswordHash;
                user.Age = newUser.Age;
                user.Gender = newUser.Gender;
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        private static string GenerateSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
