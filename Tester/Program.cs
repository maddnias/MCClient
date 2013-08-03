using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCTest;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new MCClient("127.0.0.1", 25565);
            c.Login("user", "pass");

            c.SendMessage("Greetings from uNet :-)");
          

            Console.ReadLine();
        }
    }
}
