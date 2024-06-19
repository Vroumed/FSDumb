namespace Vroumed.FSDumb.Hardware.Platforms.AWD.Modules
{
    public class LightSensor
    {
        public int Pin { get; set; } = 33;
        public int GetLightLevel() { return 0; }
        public void Setup() { }
    }
}
