using System;

namespace Vroumed.FSDumb.Extensions
{
    public static class Extensions
    {
        public static void Schedule(this object _, Action action, ushort delay = 0) 
        {
            Context.Instance.Scheduler.Schedule(action, delay);
        }
    }
}