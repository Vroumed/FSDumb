using System;
using System.Collections;
using System.Threading;
using Vroumed.FSDumb.Dependencies;

namespace Vroumed.FSDumb.Managers
{

    public enum ExecuteType
    {
        Scheduled,
        Threaded,
    }
    public class Sequence : IDependencyCandidate
    {
        [Resolved]
        private Scheduler Scheduler { get; set; }

        public ExecuteType ExecuteType { get; }
        private bool _running = false;
        private Thread _thread = null;
        public struct SequenceTask
        {
            public ushort Delay { get; init; }
            public Action Action { get; init; }
        }

        private readonly ArrayList _sequence = new ArrayList();

        public Sequence(ExecuteType type)
        {
            ExecuteType = type;
        }

        public void Stop()
        {
            _running = false;
            if (_thread != null)
                _thread.Abort();
        }

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
                Schedule(() =>
                {
                    if (_running)
                        Execute();
                });
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
            _running = true;
            switch (ExecuteType)
            {
                case ExecuteType.Scheduled:
                    foreach (SequenceTask task in _sequence)
                    {
                        Scheduler.Schedule(() =>
                        {
                            if (_running)
                                task.Action.Invoke();
                        }, task.Delay);
                    }
                    break;
                case ExecuteType.Threaded:
                    new Thread(() =>
                    {
                        ushort delay = 0;
                        foreach (SequenceTask task in _sequence)
                        {
                            ushort diff = (ushort)(task.Delay - delay);
                            delay += diff;
                            Thread.Sleep(diff); 
                            if (_running)
                                task.Action.Invoke();
                        }
                        
                    }).Start();
                    break;
            }
            
        }
    }
}
