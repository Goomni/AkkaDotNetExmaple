using Akka.Actor;

namespace ServiceA
{
    public class MyReceiveActor : ReceiveActor
    {
        public MyReceiveActor()
        {
            Receive<PingMessage>(message =>
            {
                Sender.Tell(new PongMessage(message.PingTag));
            });
        }

    }
}
