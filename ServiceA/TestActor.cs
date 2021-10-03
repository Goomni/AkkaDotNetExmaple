using Akka.Actor;
using Akka.Event;
using System;
using static Akka.Actor.FSMBase;

namespace ServiceA
{
    public class TestActor : ReceiveActor
    {
        private ILoggingAdapter logger = Context.GetLogger();

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
        protected void PingTest()
        {
            var myReceiveActor = actorSystem.ActorOf<MyReceiveActor>("MyReceiveActor");
            Console.WriteLine(myReceiveActor.Ask(new PingMessage(++pingTag)).Result); 
        }

        protected void MakeChildTest()
        {
            var myReceiveActor = actorSystem.ActorOf<MyReceiveActor>("MyReceiveActorTwo");
            Console.WriteLine(myReceiveActor.Ask(new MakeChildMessage()).Result);

            var mySon = actorSystem.ActorSelection("/user/MyReceiveActorTwo");
            Console.WriteLine(mySon.Ask(new PingMessage(++pingTag)).Result);
            Console.WriteLine(mySon.Ask(new MakeChildMessage()).Result);

            var myGrandSon = actorSystem.ActorSelection($"/user/MyReceiveActorTwo/MyReceiveActorChild{0}");
            Console.WriteLine(myGrandSon.Ask(new PingMessage(++pingTag)).Result);
        }     

        protected void BecomeTest()
        {
            var mySwapActor = actorSystem.ActorOf<SwapActor>("MySwapActor");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeHappy");
            mySwapActor.Tell("BeHappy");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeAngry");
            mySwapActor.Tell("BeHappy");
        }
        protected void WatchTest()
        {
            var targetActor = actorSystem.ActorOf<MyReceiveActor>("MyTarget");
            var watcher = actorSystem.ActorOf(WatchActor.Props(targetActor), "MyWatcher");
            Console.WriteLine(targetActor.Ask(new PingMessage(++pingTag)).Result);
            targetActor.Tell(PoisonPill.Instance, Sender);
        }
        protected async void GracefulStopTest()
        {
            var graceStopActor = actorSystem.ActorOf<GracefulStopActor>();

            try
            {
                await graceStopActor.GracefulStop(TimeSpan.FromSeconds(5), Shutdown.Instance);
            }            
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        
        public static Props Props(ActorSystem system)
        {
            return Akka.Actor.Props.Create(() => new TestActor(system));            
        }

        public void RunAll()
        {
            PingTest();
            MakeChildTest();
            WatchTest();
            GracefulStopTest();
        }
    }
}
