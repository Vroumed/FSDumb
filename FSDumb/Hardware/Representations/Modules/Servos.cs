using Vroumed.FSDumb.Hardware.Representations.Modules.Element;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class Servos
    {
        public Servo Servo1 { get; set; } = new Servo { Pin = 0 }; // PCA9685_CHANNEL_0
        public Servo Servo2 { get; set; } = new Servo { Pin = 1 }; // PCA9685_CHANNEL_1

    }
}
