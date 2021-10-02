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
                    var testActor = actorSystem.ActorOf(TestActor.Props(actorSystem), "TestActor");
                    Console.WriteLine($"Actor Created {testActor}");
                    Console.WriteLine(testActor.Ask("RunAll").Result);                    

                    if (Console.ReadKey(true).Key == ConsoleKey.X)
                    {
                        Console.WriteLine("Service A Terminating...");
                        break;
                    }
                }
            }
        }
    }
}
