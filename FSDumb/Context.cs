using System;
using System.Threading;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Runtime.Native;
using Vroumed.FSDumb.Hardware.Platforms.Freenove;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Managers;

namespace Vroumed.FSDumb
{
    internal class Context
    {
        private bool _running = false;
        public const byte RefreshRate = 30;
        public const float FrameTime = 1000f / RefreshRate;
        public float ElapsedFrameTime { get; private set; } = 0;
        public ulong Clock { get; set; }
        public Scheduler Scheduler { get; } = new Scheduler();
        public IHardwareAccessor HardwareAccessor { get; private set; }
        public event Action OnUpdate;

        public static Context Instance { get; private set; }

        public static void StartContext()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Cannot instantiate multiple time Context since it's singleton");
            }

            Instance = new Context();
            Instance.HardwareAccessor = new FreenoveHardware();
        }

        public static Sequence StartSequence()
        {
            return new Sequence();
        }

        public void StartMainLoop()
        {
            if (_running)
            {
                throw new InvalidOperationException("Cannot Run an already running app");
            }

            while (true)
            {
                Clock = HighResTimer.GetCurrent();
                Scheduler.ExecuteTasks();
                OnUpdate?.Invoke();
                ElapsedFrameTime = HighResTimer.GetCurrent() - Clock;
                if (ElapsedFrameTime < FrameTime)
                {
                    Thread.Sleep((int)(FrameTime - ElapsedFrameTime));
                }

            }
        }
    }
}
