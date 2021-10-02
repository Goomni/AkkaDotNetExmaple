using Akka.Actor;
using Akka.Event;

namespace ServiceA
{
    public class WatchActor : UntypedActor
    {
        private ILoggingAdapter logger = Context.GetLogger();
        private IActorRef child;
        private IActorRef lastSender = Context.System.DeadLetters;
        public WatchActor(IActorRef targetActor)
        {
            child = targetActor;
            Context.Watch(targetActor);
        }
        protected override void OnReceive(object message)
        {
            logger.Warning($"WatchActor OnReceive {message}");

            switch(message as string)
            {
                case "kill":
                    Context.Stop(child);
                    lastSender = Sender;
                    break;
                default:
                    break;
            }

            if(message is Terminated terminated)
            {
                if(terminated.Equals(child))
                {
                    logger.Error($"My TargetChild terminated.....");
                }
            }
        }

        public static Props Props(IActorRef target)
        {
            return Akka.Actor.Props.Create(() => new WatchActor(target));
        }

    }
}
