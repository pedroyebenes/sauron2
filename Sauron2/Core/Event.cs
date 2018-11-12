using System;
namespace Sauron2.Core
{
    public class Event
    {
        internal static ulong IDCount = 0;

        public ulong ID { get; private set; }
        public ulong Time { get; set; }

        public Module SrcModule { get; set; }
        public Module DestModule { get; set; }
        public int SrcPort { get; set; }
        public int DestPort { get; set; }

        public Event()
        {
            this.ID = IDCount;
            IDCount += 1;
        }

        public Event(ulong time, Module module, int port = -1)
        {
            this.ID = IDCount;
            IDCount += 1;
            this.Time = time;
            this.SrcModule = module;
            this.SrcPort = port;
        }
    }
}
