using System;
using System.Device.Wifi;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Networking;
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
        public bool IsOnline { get; private set; } = false;
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
            Instance.Clock = HighResTimer.GetCurrent();
            Instance.HardwareAccessor = new FreenoveHardware();
            Instance.HardwareAccessor.Lighting.BusyLights();
            StartSequence(ExecuteType.Threaded).Schedule(Instance.InitializeInternet).Execute();
        }

        public void InitializeInternet()
        {
            CancellationTokenSource cs = new(10000);
            IsOnline = WifiNetworkHelper.ConnectFixAddress(InternetConfiguration.WifiSSID, InternetConfiguration.Password, token: cs.Token, ipConfiguration:new IPConfiguration("192.168.1.7", "255.255.255.0", "192.168.1.1"));
            if (!IsOnline)
            {
                Debug.WriteLine($"Can't connect to the network, error: {WifiNetworkHelper.Status}");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException}");
                }
                StartSequence(ExecuteType.Threaded)
                    .Schedule(() =>
                    {
                        HardwareAccessor.Buzzer.Frequency = 200;
                        HardwareAccessor.Buzzer.Start();
                        HardwareAccessor.Lighting.TurnOff();
                    }).Delay(100)
                    .Schedule(HardwareAccessor.Buzzer.Stop)
                    .Delay(100)
                    .Schedule(HardwareAccessor.Buzzer.Start)
                    .Delay(100)
                    .Schedule(HardwareAccessor.Buzzer.Stop)
                    .Execute();
            }
            else
            {
                HardwareAccessor.Buzzer.Ping();
                HardwareAccessor.Lighting.StandardLights();
            }
        }

        public static Sequence StartSequence(ExecuteType type)
        {
            return new Sequence(type);
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
