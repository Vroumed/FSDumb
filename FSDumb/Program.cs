using System;
using System.Collections;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Drawing;
using System.Threading;
using Iot.Device.Ws28xx.Esp32;
using nanoFramework.Hardware.Esp32;
using Vroumed.FSDumb.Hardware.Representations;

namespace Vroumed.FSDumb
{
    public class Program
    {
        public static void Main()
        {
            Context ctx = new Context();
            ctx.StartMainLoop();
        }
    }
}
