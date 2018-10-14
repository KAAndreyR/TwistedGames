using System;

namespace TwistedGames.Core.Common
{
    public class DiscreteCyclesCounter
    {
        private long CycleDuration { get; }
        private DateTime StartTime { get; set; }
        private DateTime LastGameUpdateTime { get; set; }

        public DiscreteCyclesCounter() : this(TimeSpan.FromSeconds(1)) { }

        public DiscreteCyclesCounter(TimeSpan cycleDuration)
        {
            CycleDuration = cycleDuration.Ticks;
            Restart();
        }

        public int GetGameTicksCount()
        {
            long CountFullCycles(DateTime dateTime)
            {
                return (dateTime - StartTime).Ticks / CycleDuration;
            }

            var now = DateTime.Now;
            var result = CountFullCycles(now) - CountFullCycles(LastGameUpdateTime);
            LastGameUpdateTime = now;
            return (int)result;
        }

        public void Restart()
        {
            StartTime = DateTime.Now;
            LastGameUpdateTime = DateTime.Now;
        }
    }
}