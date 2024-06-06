﻿using System.Drawing;
using Vroumed.FSDumb.Hardware.Representations.Modules.Element;
using Iot.Device.Ws28xx.Esp32;

namespace Vroumed.FSDumb.Hardware.Controllers
{
    public class LedController
    {
        private Ws28xx _strip;

        public byte Pin { get; } = 32;

        public LedController()
        {
            _strip = new Ws2812c(Pin, 12);
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
