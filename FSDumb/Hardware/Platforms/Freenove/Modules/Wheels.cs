
using Vroumed.FSDumb.Hardware.Controllers;
using Vroumed.FSDumb.Hardware.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules
{
    public class Wheels
    {
        public Wheels()
        {
            TopLeft = new Wheel(Controller, 14, 15);
            TopRight = new Wheel(Controller, 12, 13);
            BottomLeft = new Wheel(Controller, 8, 9);
            BottomRight = new Wheel(Controller, 10, 11);
            Controller = new MotorController(I2CDataPin, I2CClockPin, I2CDeviceAddress);

            TopLeft.Move(1);
        }

        public byte I2CDataPin { get; } = 13;
        public byte I2CClockPin { get; } = 14;
        public byte I2CDeviceAddress { get; } = 0x5F;
        private MotorController Controller { get; }
        public Wheel TopLeft { get; }
        public Wheel TopRight { get; }
        public Wheel BottomLeft { get; }
        public Wheel BottomRight { get; }

    }
}
