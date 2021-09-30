using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public class TestActor
    {
        protected ActorSystem actorSystem;

        private int pingTag = 0;
        public TestActor(ActorSystem actorSystem)
        {
            this.actorSystem = actorSystem;
        }
        protected void FirstTest()
        {
            var myReceiveActor = actorSystem.ActorOf<MyReceiveActor>("myReceiveActor");
            Console.WriteLine(myReceiveActor.Ask(new PingMessage(++pingTag)).Result); 
        }
        public void RunAll()
        {
            FirstTest();
        }
    }
}
