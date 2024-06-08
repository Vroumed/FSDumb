using System.Drawing;
using Vroumed.FSDumb.Extensions;
using Vroumed.FSDumb.Hardware.Controllers;
using Vroumed.FSDumb.Hardware.Modules;
using Vroumed.FSDumb.Hardware.Representations.Modules;
using Vroumed.FSDumb.Managers;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules
{
    public class LEDs : ILighting
    {
        public LED[] LedArray { get; set; } = new LED[12];
        private LedController _ledController;
        public Sequence CurrentSequence { get; private set; }
        public byte LightCount => 12;

        public LEDs()
        {
            _ledController = new LedController();
            for (byte i = 0; i < LightCount; i++)
            {
                LedArray[i] = new LED(_ledController) { Index = i };
            }
        }

        public void Commit()
        {
            _ledController.Commit();
        }

        public void SetLight(int lightIndex, Color color)
        {
            LedArray[lightIndex].SetColor(color);
        }

        public void TurnOff()
        {
            for (byte i = 0; i < LightCount; i++)
            {
                LedArray[i].TurnOff();
            }
        }

        public void Fill(Color color)
        {
            for (byte i = 0; i < LightCount; i++)
            {
                LedArray[i].SetColor(color);
            }
        }

        public void BusyLights()
        {
            CurrentSequence?.Stop();

            Color hig = Color.Red;
            Color mid = Color.FromArgb(255, 170, 0, 0);
            Color low = Color.FromArgb(255, 80, 0, 0);

            byte i = 0;
            CurrentSequence = Context.StartSequence(ExecuteType.Scheduled).Schedule(() =>
            {
                TurnOff();
                LedArray[GetPositiveModulo(i, LightCount)].SetColor(hig);
                LedArray[GetPositiveModulo(i - 1, LightCount)].SetColor(mid);
                LedArray[GetPositiveModulo(i - 2, LightCount)].SetColor(low);

                if (i == byte.MaxValue)
                    i = 0;
                else
                    i++;
                Commit();
            }).Delay(100).Loop();
            CurrentSequence.Execute();
            return;

            byte GetPositiveModulo(int i, byte modulo)
            {
                int res = i;
                while (res < 0)
                {
                    res = (short)(modulo - i);
                }

                return (byte)(res % modulo);
            }
        }

        public void StandardLights()
        {
            CurrentSequence?.Stop();
            this.Schedule(() =>
            {
                for (byte i = 0; i < LightCount; i++)
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
    }
}
