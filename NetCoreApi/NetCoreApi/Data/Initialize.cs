using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.ApplicationInsights.Extensibility.Implementation;
using NetCoreApi.Models;

namespace NetCoreApi.Data
{
    public class Initialize
    {
        public static void Initializer(UserContext context)
        {
            context.Database.EnsureCreated();

            //Look for any students.
            if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

            var users = new User[]
            {
                new User{UserName = "roman"},
                new User{UserName = "Ruslan"},
                new User{UserName = "Evgen"},

            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

           
        }
    }
}
