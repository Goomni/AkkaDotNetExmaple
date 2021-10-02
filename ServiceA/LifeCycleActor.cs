using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public class LifeCycleActor : UntypedActor
    {
        private ILoggingAdapter log = Context.GetLogger();
        protected override void OnReceive(object message)
        {
            log.Warning($"LifeCycleActor OnReceive {message}");
            Sender.Tell($"LifeCycleActor {message} Received");
        }

        protected override void PostRestart(Exception reason)
        {
            log.Error($"LifeCycleActor PostRestart {reason}");
        }

        protected override void PostStop()
        {
            log.Error($"LifeCycleActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            log.Error($"LifeCycleActor PreRestart {reason} {message}");
        }
        protected override void PreStart()
        {
            log.Error($"LifeCycleActor PreStart");
        }      

    }
}
