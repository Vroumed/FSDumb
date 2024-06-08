using System;
using System.Device.Pwm;
using Iot.Device.Pwm;
using Vroumed.FSDumb.Hardware.Controllers;

namespace Vroumed.FSDumb.Hardware.Modules
{

    public class Wheel
    {
        public Wheel(MotorController controller, int forwardChannel, int backwardChannel)
        {
            Controller = controller;
            ForwardChannel = forwardChannel;
            BackwardChannel = backwardChannel;

            _forwardPwm = controller.CreateChannel(forwardChannel);
            _backwardPwm = controller.CreateChannel(backwardChannel);
        }

        private Pca9685PwmChannel _forwardPwm { get; }
        private Pca9685PwmChannel _backwardPwm { get; }

        public MotorController Controller { get; }

        public int ForwardChannel { get; }
        public int BackwardChannel { get; }
        public void Move(float speed)
        {
            if (speed > 0)
            {
                _forwardPwm.Frequency = (int)(Math.Clamp(Math.Abs(speed), 0, 1) * 255);
            }
        }
        public void Stop()
        {

        }
    }
}
