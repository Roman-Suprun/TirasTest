using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NetCoreApi.Models;
using Newtonsoft.Json.Linq;

namespace NetCoreApi.Controllers
{
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public UserContext context;
        public ValuesController(UserContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        //// GET api/values/5
        [HttpGet("getusers")]
        public string Get(int id)
        {
            List<User> users = context.Users.ToList();
            foreach (User us in users)
            {
                Console.Write(us.UserName);
            }
            string json = JsonConvert.SerializeObject(new
            {
                result = users
            });
        return json;
        }

        // POST api/values
        [HttpPost("pd")]
        public void PushData([FromBody]JObject value)
        {
            User us = new User
            {
                UserName = value.First.Last.ToString()
            };
            context.Users.Add(us);
            context.SaveChanges();
        }
        
        
    }
}
