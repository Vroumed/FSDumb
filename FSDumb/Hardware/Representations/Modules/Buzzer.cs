using nanoFramework.Hardware.Esp32;
using System.Device.Pwm;
using System.Threading;
using Vroumed.FSDumb.Extensions;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class Buzzer
    {
        public PwmChannel Channel { get; private set; }
        public int Pin { get; set; } = 2;

        public Buzzer()
        {
            Configuration.SetPinFunction(Pin, DeviceFunction.PWM3);
            Channel = PwmChannel.CreateFromPin(Pin);

            this.Schedule(Ping);
        }

        public void Ping()
        {
            var seq = Context.StartSequence()
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
