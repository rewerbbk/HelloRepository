using System;
using System.Diagnostics;

namespace Utility
{
    public static class TimeKeeper
    {
        public static TimeSpan Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            return watch.Elapsed;
        }
    }
}
