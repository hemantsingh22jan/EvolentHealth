using EvolentHeath.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EvolentHealth.API.Controllers
{
    public class UserController : ApiController
    {
        EvolentContext context = new EvolentContext();

        [HttpPost]
        public int PostAddUser([FromBody] User user)
        {

            User obj = new User();
            if (user != null)
            {
                context.Users.Add(user);
                context.SaveChanges();

                return user.UserId;
            }
            else
                return 0;
        }

        [HttpGet]
        public User GetUser(int id)
        {
            User user = context.Users.Find(id);
            return user;
        }

        [HttpGet]
        public List<User> GetUsersList()
        {

            return context.Users.ToList();
        }

        [HttpPut]
        public IHttpActionResult PutModifyUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("User details are not valid");
            using (var ctx = new EvolentContext())
            {
                var existingUser = ctx.Users.Where(rec => rec.UserId == user.UserId)
                                   .FirstOrDefault<User>();

                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Address = user.Address;
                    existingUser.ContactNo = user.ContactNo;

                    ctx.Entry(existingUser).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public void DeleteUser(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }

        }
    }
}
