using Akka.Actor;
using System;

namespace ServiceA
{
    public class MyReceiveActor : ReceiveActor
    {
        private long childIndex;
        public MyReceiveActor()
        {
            guid = System.Guid.NewGuid();
            Receive<PingMessage>(message =>
            {
                Sender.Tell(new PongMessage(message.PingTag));
            });

            Receive<MakeChildMessage>(message =>
            {
                Context.ActorOf<MyReceiveActor>($"MyReceiveActorChild{childIndex++}");
                Sender.Tell($"MyReceiveActorChild Created {childIndex}");
            });
        }

    }
}
