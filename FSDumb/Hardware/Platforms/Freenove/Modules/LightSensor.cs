namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules
{
    public class LightSensor
    {
        public int Pin { get; set; } = 33;
        public int GetLightLevel() { return 0; }
        public void Setup() { }
    }
}
