namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class LightSensor
    {
        public int Pin { get; set; } = 33;
        public int GetLightLevel() { return 0; }
        public void Setup() { }
    }
}
