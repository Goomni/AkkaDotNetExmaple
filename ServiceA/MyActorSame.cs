using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public class MyActorSame : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if(message is PingMessage pingMessage)
            {
                Sender.Tell(new PongMessage(pingMessage.PingTag));
            }
            else if (message is MakeChildMessage)
            {
                var tempChild = Context.ActorOf<MyActorSame>();
                Sender.Tell($"MakeChildMessage : {tempChild}");
            }
        }
    }

}
