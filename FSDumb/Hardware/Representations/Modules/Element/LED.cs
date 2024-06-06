using System.Drawing;
using Vroumed.FSDumb.Hardware.Controllers;

namespace Vroumed.FSDumb.Hardware.Representations.Modules.Element
{
    public class LED
    {
        private readonly LedController _ledController;

        public LED(LedController ledController)
        {
            _ledController = ledController;
        }

        public ushort Index { get; set; }

        public void SetColor(Color color)
        {
            _ledController.SetColor(this, color);
        }

        public void TurnOff()
        {
            _ledController.SetColor(this, Color.Black);
        }
    }
}