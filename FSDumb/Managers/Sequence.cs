using System;
using System.Collections;

namespace Vroumed.FSDumb.Managers
{
    public class Sequence
    {
        public struct SequenceTask
        {
            public ushort Delay { get; init; }
            public Action Action { get; init; }
        }

        private readonly ArrayList _sequence = new ArrayList();
        public ushort CurrentDelay { get; private set; } = 0;

        public Sequence Delay(ushort offset)
        {
            CurrentDelay += offset;
            return this;
        }

        public Sequence Schedule(Action action)
        {
            _sequence.Add(new SequenceTask
            {
                Delay = CurrentDelay,
                Action = action
            });

            return this;
        }

        public Sequence Loop(byte count = 0)
        {
            if (count <= 0)
            {
                Schedule(Execute);
                return this;
            }
            ushort delay = 0;
            for (int i = 0; i < count; i++)
            {
                foreach (SequenceTask task in _sequence)
                {
                    Delay((ushort)(task.Delay - delay));
                    Schedule(task.Action);
                    delay += task.Delay;
                }
            }

            return this;
        }

        public void Execute()
        {
            foreach (SequenceTask task in _sequence)
            {
                Context.Instance.Scheduler.Schedule(task.Action, task.Delay);
            }
        }
    }
}
