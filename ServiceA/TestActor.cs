using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public class TestActor : ReceiveActor
    {
        protected ActorSystem actorSystem;

        private int pingTag = 0;
        public TestActor(ActorSystem actorSystem)
        {
            this.actorSystem = actorSystem;

            Receive<string>(x =>
            {
                switch (x)
                {
                    case "RunAll":
                        RunAll();
                        Sender.Tell("Run All Executed");
                        break;
                    default:
                        Sender.Tell("Invalid Message");
                        break;
                }
            });
        }
        protected void FirstTest()
        {
            var myReceiveActor = actorSystem.ActorOf<MyReceiveActor>("MyReceiveActor");
            Console.WriteLine(myReceiveActor.Ask(new PingMessage(++pingTag)).Result); 
        }

        protected void SecondTest()
        {
            var myReceiveActor = actorSystem.ActorOf<MyReceiveActor>("MyReceiveActorTwo");
            Console.WriteLine(myReceiveActor.Ask(new MakeChildMessage()).Result);

            var mySon = actorSystem.ActorSelection("/user/MyReceiveActorTwo");
            Console.WriteLine(mySon.Ask(new PingMessage(++pingTag)).Result);
            Console.WriteLine(mySon.Ask(new MakeChildMessage()).Result);

            var myGrandSon = actorSystem.ActorSelection($"/user/MyReceiveActorTwo/MyReceiveActorChild{0}");
            Console.WriteLine(myGrandSon.Ask(new PingMessage(++pingTag)).Result);
        }     

        protected void ThirdTest()
        {
            var mySwapActor = actorSystem.ActorOf<SwapActor>("MySwapActor");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeHappy");
            mySwapActor.Tell("BeHappy");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeHappy");
        }
        protected void FourthTest()
        {
            
        }
        
        public static Props Props(ActorSystem system)
        {
            return Akka.Actor.Props.Create(() => new TestActor(system));            
        }

        public void RunAll()
        {
            FirstTest();
            SecondTest();
            ThirdTest();
        }
    }
}
