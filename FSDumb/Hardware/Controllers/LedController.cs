using System.Drawing;
using Iot.Device.Ws28xx.Esp32;
using Vroumed.FSDumb.Hardware.Modules;

namespace Vroumed.FSDumb.Hardware.Controllers
{
    public class LedController
    {
        private Ws28xx _strip;

        public byte Pin { get; }

        public LedController(byte pin, byte ledCount)
        {
            _strip = new Ws2812c(Pin = pin, ledCount);
        }

        public void SetColor(LED led, Color color)
        {
            _strip.Image.SetPixel(led.Index, 0, color);
        }

        public void Commit()
        {
            _strip.Update();
        }
    }
}
