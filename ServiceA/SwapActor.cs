using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public class SwapActor : UntypedActor
    {
        private ILoggingAdapter log = Context.GetLogger();
        protected override void OnReceive(object message)
        {
            log.Info($"NORMAL : {message}");
            switch(message as string)
            {
                case "BeAngry":
                    log.Info($"{this} Rellay Angry!!");                    
                    Become(Angry);
                    break;
                case "BeHappy":
                    log.Info($"{this} Rellay Happy!!");
                    Become(Happy);
                    break;

                    
            }
        }
        protected void Angry(object message)
        {
            log.Info($"Angry : {message}");
            switch (message as string)
            {
                case "BeAngry":
                    log.Info($"{this} Duplicated Rellay Angry!!");
                    Sender.Tell($"{this} Duplicated Rellay Angry!!");
                    break;
                case "BeHappy":
                    log.Info($"{this} Rellay Happy!!");
                    Become(Happy);
                    break;


            }
        }
        protected void Happy(object message)
        {
            log.Info($"Happy : {message}");
            switch (message as string)
            {
                case "BeAngry":
                    log.Info($"{this} Rellay Angry!!");
                    Become(Angry);
                    break;
                case "BeHappy":
                    log.Info($"{this} Duplicated Rellay Happy!!");
                    Sender.Tell($"{this} Duplicated Rellay Happy!!");
                    break;


            }
        }
    }
}
