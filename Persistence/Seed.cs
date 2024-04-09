using Koleo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>
            {
                new User
                {
                    Name = "Sigma",
                    Surname = "Sigmiarski",
                    Email = "sigma@pw.edu.pl",
                    Password = "12345678",
                    CardNumber = "11111111111111111111111111",
                },

                //new User
                //{
                //    Name = "Sigma2",
                //    Surname = "Sigmiarski2",
                //    Email = "sigma2@pw.edu.pl",
                //    Password = "12345678",
                //    CardNumber = "11111111111111111111111111",
                //},

                //new User
                //{
                //    Name = "Sigma3",
                //    Surname = "Sigmiarski3",
                //    Email = "sigma3@pw.edu.pl",
                //    Password = "12345678",
                //    CardNumber = "11111111111111111111111111",
                //},

            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();


        }
    }
}
