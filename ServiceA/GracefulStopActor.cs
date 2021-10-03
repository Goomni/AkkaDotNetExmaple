using Akka.Actor;
using Akka.Event;
using static Akka.Actor.FSMBase;

namespace ServiceA
{
    public class GracefulStopActor : UntypedActor
    {
        private ILoggingAdapter logger = Context.GetLogger();

        private IActorRef worker = Context.Watch(Context.ActorOf<SwapActor>("WorkerSwapActor"));
        protected override void OnReceive(object message)
        {
            logger.Info($"<GracefulStopActor> Normal State : {message}");
            switch (message)
            {
                case "BeAngry":
                    worker.Tell("BeAngry");
                    break;
                case Shutdown s:
                    worker.Tell(PoisonPill.Instance, Self);
                    Context.Become(ShuttingDown);
                    break;
            }
        }

        protected override void PostStop()
        {
            logger.Info($"<GracefulStopActor> PostStop State");
        }

        private void ShuttingDown(object message)
        {
            logger.Info($"<GracefulStopActor> ShuttingDown State : {message}");
            switch (message)
            {
                case "job":
                    Sender.Tell("Service unavailble, shutting down", Self);
                    break;
                case Terminated t:
                    Context.Stop(Self);
                    break;
            }
        }
    }
}
