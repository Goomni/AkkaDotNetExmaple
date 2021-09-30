using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    public abstract class Message {}

    public class PingMessage : Message
    {
        public PingMessage(int pingTag)
        {
            this.PingTag = pingTag;
        }

        public int PingTag { get; private set; }

        public override string ToString()
        {
            return $"PingTag #{PingTag}";
        }
    }
    public class PongMessage : Message
    {
        public PongMessage(int pongTag)
        {
            this.PongTag = pongTag;
        }

        public int PongTag { get; private set;  }

        public override string ToString()
        {
            return $"PongTag #{PongTag}";
        }
    }
    public class MakeChildMessage : Message
    {

    }
}
