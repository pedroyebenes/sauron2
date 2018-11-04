using System;
namespace Sauron2
{
    public class Event
    {
        private static ulong IDCount = 0;

        protected ulong Time;
        protected ulong ID;

        public Event(ulong time)
        {
            this.Time = time;
            this.ID = IDCount;
            IDCount += 1;
        }

        public ulong GetTime()
        {
            return Time;
        }

        public ulong GetID()
        {
            return ID;
        }
    }
}
