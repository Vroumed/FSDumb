using System;
using System.Device.Gpio;
using System.Drawing;
using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Extensions;
using Vroumed.FSDumb.Hardware.Controllers;
using Vroumed.FSDumb.Hardware.Modules;
using Vroumed.FSDumb.Hardware.Representations.Modules;
using Vroumed.FSDumb.Managers;
using Vroumed.FSDumb.Utils;

namespace Vroumed.FSDumb.Hardware.Platforms.RWD.Modules
{
    public class LightingManager : ILighting, IDependencyCandidate
    {
        [Resolved]
        private Scheduler Scheduler { get; set; }
        
        [Resolved]
        private Context Context { get; set; }

        private static byte lightCount = 7;
        public LED[] FrontLeds { get; } = new LED[2];
        public LED[] RearLeds { get; } = new LED[3];

        public LED ServerStatusLed { get; private set; }
        public LED ControllerStatusLed { get; private set; }

        private LedController _ledController;
        public Sequence ServerSequence { get; private set; }
        public Sequence ControllerSequence { get; private set; }
        public byte LightCount => lightCount;
        public static GpioPin Pin;

        public LightingManager()
        {
            _ledController = new LedController(32, LightCount);
            FrontLeds[0] = new LED(_ledController) { Index = 0 };
            ServerStatusLed = new LED(_ledController) { Index = 1 };
            ControllerStatusLed = new LED(_ledController) { Index = 2 };
            FrontLeds[1] = new LED(_ledController) { Index = 3 };

            for (int i = 0; i < 3; i++)
            {
                RearLeds[i] = new LED(_ledController) { Index = (ushort)(i + 4) };
            }
        }

        public void Commit()
        {
            _ledController.Commit();
        }

        public void TurnOff()
        {
            for (byte i = 0; i < FrontLeds.Length; i++)
            {
                FrontLeds[i].TurnOff();
            }
            
            for (byte i = 0; i < RearLeds.Length; i++)
            {
                RearLeds[i].TurnOff();
            }
        }

        public void Fill(Color color)
        {
            FrontLeds.Fill(color);
            RearLeds.Fill(color);
        }

        public void ConnectingServer()
        {
            ServerSequence?.Stop();
            ServerSequence = Context.StartSequence(ExecuteType.Scheduled);
            SetLedLoading(ServerStatusLed, ServerSequence);
        }

        public void ErrorServer()
        {
            ServerSequence?.Stop();
            Scheduler.Schedule(() => ServerStatusLed.SetColor(Color.Red));
        }

        public void ServerOnline()
        {
            ServerSequence?.Stop();
            Scheduler.Schedule(() => ServerStatusLed.SetColor(Color.LawnGreen));
        }

        public void ConnectingController()
        {
            ControllerSequence?.Stop();
            ControllerSequence = Context.StartSequence(ExecuteType.Scheduled);
            SetLedLoading(ControllerStatusLed, ControllerSequence);
        }

        public void ErrorController()
        {
            throw new NotImplementedException();
        }

        public void ControllerOnline()
        {
            throw new NotImplementedException();
        }

        public void SetLedLoading(LED led, Sequence seq)
        {
            ulong start = Context.Clock;
            ushort step = 1000;

            seq = Context.StartSequence(ExecuteType.Scheduled).Schedule(() =>
            {
                ulong currentTime = Context.Clock;
                ushort iterCount = (ushort)Math.Floor(currentTime / start);
                bool des = iterCount % 2 == 0;
                byte fro = (byte)(des ? 0 : 255);
                byte to = (byte)(des ? 255 : 0);

                float ratio = ((currentTime-start) % step) / (float)step;
                Color color = Color.FromArgb((int)MathUtils.Lerp(fro, to, ratio), 0, 0);
                led.SetColor(color);
                Commit();
            }).Delay((ushort)Context.FrameTime).Loop();
            seq.Execute();
        }

        public void StandardLights()
        {
            Scheduler.Schedule(() =>
            {
                FrontLeds.Fill(Color.Blue);
                RearLeds.Fill(Color.Red);
                Commit();
            });
        }
    }
}
