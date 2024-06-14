using System;
using System.Collections;
using Vroumed.FSDumb.Dependencies;

namespace Vroumed.FSDumb.Managers
{
    public class Scheduler : IDependencyCandidate
    {
        [Resolved]
        private Context Context { get; set; }

        public class Task
        {
            public Action Action { get; }
            public ulong ExecutionTime { get; }

            public Task(Action action, ulong executionTime)
            {
                Action = action;
                ExecutionTime = executionTime;
            }
        }
        
        private ArrayList _queue { get; } = new ArrayList();

        public void ExecuteTasks()
        {
            ulong now = Context.Clock;
            foreach (Task task in _queue.ToArray())
            {
                if (now > task.ExecutionTime)
                {
                    task.Action.Invoke();
                    _queue.Remove(task);
                }
            }
        }

        /// <summary>
        /// Used to delay an action syncronously in a non blocking way, or to inject back to the Main Thread
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        public void Schedule(Action action, ushort delay = 0)
        {
            Task task = new Task(action, Context.Clock + delay);
            _queue.Add(task);
        }
    }
}
