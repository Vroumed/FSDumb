using System.Drawing;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface ILighting
    {
        public byte LightCount { get; }
        public void SetLight(int lightIndex, Color color);
        public void TurnOff();
        public void Fill(Color color);
    }
}
