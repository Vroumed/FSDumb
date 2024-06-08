using System;
using System.Device.I2c;
using Iot.Device.Pwm;
using nanoFramework.Hardware.Esp32;
using Vroumed.FSDumb.lib;

namespace Vroumed.FSDumb.Hardware.Controllers
{
    public class MotorController
    {
        private readonly Pca9685 _pca9685;
        public MotorController(byte dataPin, byte clockPin, byte adress)
        {
            Configuration.SetPinFunction(dataPin, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(clockPin, DeviceFunction.I2C1_CLOCK);
            I2cConnectionSettings i2cConnectionSettings = new(1, adress);
            I2cDevice device = new I2cDevice(i2cConnectionSettings);

            _pca9685 = new Pca9685(device, 0, 0);
        }

        public Pca9685PwmChannel CreateChannel(int channel)
        {
            return _pca9685.CreatePwmChannel(channel);
        }
    }
}
