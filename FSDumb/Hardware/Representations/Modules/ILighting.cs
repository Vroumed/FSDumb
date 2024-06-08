using System.Drawing;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface ILighting
    {
        public byte LightCount { get; }
        /// <summary>
        /// change a single's light Color.
        /// Don't forget to <see cref="Commit"/> to apply colors
        /// </summary>
        /// <param name="lightIndex"></param>
        /// <param name="color"></param>
        public void SetLight(int lightIndex, Color color);
        public void TurnOff();
        public void Commit();
        public void Fill(Color color);

        public void BusyLights();
        public void StandardLights();
    }
}
