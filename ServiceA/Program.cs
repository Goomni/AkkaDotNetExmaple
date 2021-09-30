using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    internal class Program
    {
        static Program()
        {
            Console.CancelKeyPress += (sender, cancelArgs) =>
            {
                cancelArgs.Cancel = true;
            };
        }

        static void Main(string[] args)
        {
            using (var actorSystem = ActorSystem.Create("ServiceA"))
            {
                while(true)
                {
                    var actorTest = new TestActor(actorSystem);
                    actorTest.RunAll();

                    if(Console.ReadKey(true).Key == ConsoleKey.X)
                    {
                        Console.WriteLine("Service A Terminating...");
                        break;
                    }
                }
            }
        }
    }
}
