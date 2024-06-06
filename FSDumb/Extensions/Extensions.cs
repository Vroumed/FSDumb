using System;

namespace Vroumed.FSDumb.Extensions
{
    public static class Extensions
    {
        public static void Schedule(this object _, Action action, ushort delay = 0) 
        {
            Context.Instance.Scheduler.Schedule(action, delay);
        }

        public static void OnUpdate(this object _, Action action)
        {
            Context.Instance.OnUpdate += action;
        }
    }
}