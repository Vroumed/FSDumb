using System;
using System.Device.Wifi;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Networking;
using nanoFramework.Runtime.Native;
using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Hardware.Platforms.Freenove;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Managers;
using Vroumed.FSDumb.Online;

namespace Vroumed.FSDumb
{
    public class Context
    {
        private bool _running = false;
        public const byte RefreshRate = 30;
        public const float FrameTime = 1000f / RefreshRate;
        public bool IsOnline { get; private set; } = false;
        public float ElapsedFrameTime { get; private set; } = 0;
        public ulong Clock { get; set; }
        private Scheduler Scheduler { get; } = new();
        private IHardwareAccessor HardwareAccessor { get; set; }
        private event Action OnUpdate;

        private Connector Connector { get; }
        private DependencyInjector DependencyInjector { get; } = new DependencyInjector();

        public Context()
        {
            DependencyInjector.Cache(this);
            DependencyInjector.Cache(Scheduler);
            HardwareAccessor = DependencyInjector.Resolve<FreenoveHardware>();
            DependencyInjector.CacheAs(HardwareAccessor);
            HardwareAccessor.Lighting.BusyLights();
            Connector = DependencyInjector.Resolve<Connector>();
        }


        public Sequence StartSequence(ExecuteType type)
        {
            return DependencyInjector.Resolve<Sequence>();
        }

        public void StartMainLoop()
        {
            if (_running)
            {
                throw new InvalidOperationException("Cannot Run an already running app");
            }


            //StartSequence(ExecuteType.Threaded).Schedule(Connector.Run).Execute();

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
