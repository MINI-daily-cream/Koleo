using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application
{
    public class TestKlasa
    {
        private readonly IServiceProvider _serviceProvider;
        public TestKlasa(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Console.WriteLine("JEJEJEJEJEJEs");
        }

        public void rob()
        {
            var context = _serviceProvider.GetRequiredService<DataContext>();
            var user = context.Users.ToList().FirstOrDefault();
            Console.WriteLine(user?.Name);        
            Console.WriteLine("zrobilem");
        }
    }
}