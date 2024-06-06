using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Representations
{
    public class HardwareAccessor
    {
        public Wheels Wheels { get; } = new Wheels();
        public Servos Servos { get; } = new Servos();
        public Buzzer Buzzer { get; } = new Buzzer();
        public Battery Battery { get; } = new Battery();
        public LightSensor LightSensor { get; } = new LightSensor();
        public TrackSensor TrackSensor { get; } = new TrackSensor();
        public UltrasonicSensor UltrasonicSensor { get; } = new UltrasonicSensor();
        public LEDs LEDs { get; } = new LEDs();

    }
}
