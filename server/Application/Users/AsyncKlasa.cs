using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users
{
    public class AsyncKlasa
    {
        public async Task asyncFunkcja()
        {
            await Task.Run(async () =>
            {
                // await Task.Delay(5000);
                Console.WriteLine("dobra");
            });
            while (true)
            {
                Console.WriteLine("heja");
            }
        }
    }
}