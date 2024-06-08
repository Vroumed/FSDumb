using nanoFramework.Hardware.Esp32;
using System.Device.Pwm;
using System.Drawing;
using System.Threading;
using Vroumed.FSDumb.Extensions;
using Vroumed.FSDumb.Hardware.Representations.Modules;
using Vroumed.FSDumb.Managers;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules
{
    public class Buzzer : IBuzzer
    {
        public PwmChannel Channel { get; private set; }
        public int Pin { get; set; } = 2;

        public Buzzer()
        {
            Configuration.SetPinFunction(Pin, DeviceFunction.PWM1);
            Channel = PwmChannel.CreateFromPin(Pin);
        }

        public void Start()
        {
            Channel.Start();
        }

        public void Stop()
        {
            Channel.Stop();
        }

        public int Frequency
        {
            get => Channel.Frequency;
            set => Channel.Frequency = value;
        }

        public void Ping()
        {
            var seq = Context.StartSequence(ExecuteType.Threaded)
                .Schedule(() =>
                {
                    Channel.Frequency = 400;
                    Channel.Start();
                })
                .Delay(100)
                .Schedule(() =>
                {
                    Channel.Frequency = 1200;
                })
                .Delay(100)
                .Schedule(() =>
                {
                    Channel.Stop();
                });

            seq.Execute();
        }
    }
}
