using System.Drawing;
using Vroumed.FSDumb.Extensions;
using Vroumed.FSDumb.Hardware.Controllers;
using Vroumed.FSDumb.Hardware.Representations.Modules.Element;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class LEDs
    {
        public LED[] LedArray { get; set; } = new LED[12];
        private LedController _ledController;

        public LEDs()
        {
            _ledController = new LedController();
            for (byte i = 0; i < 12; i++)
            {
                LedArray[i] = new LED(_ledController) { Index = i };
            }


            this.Schedule(() =>
            {
                for (byte i = 0; i < 12; i++)
                {
                    LedArray[i].TurnOff();
                }

                LedArray[0].SetColor(Color.Orange);
                LedArray[1].SetColor(Color.Blue);
                LedArray[2].SetColor(Color.Blue);
                LedArray[3].SetColor(Color.Blue);
                LedArray[4].SetColor(Color.Blue);
                LedArray[5].SetColor(Color.Orange);
                LedArray[6].SetColor(Color.Orange);
                LedArray[7].SetColor(Color.Red);
                LedArray[8].SetColor(Color.Red);
                LedArray[9].SetColor(Color.Red);
                LedArray[10].SetColor(Color.Red);
                LedArray[11].SetColor(Color.Orange);
                Commit();
            });
        }

        public void Commit()
        {
            _ledController.Commit();
        }
    }
}
