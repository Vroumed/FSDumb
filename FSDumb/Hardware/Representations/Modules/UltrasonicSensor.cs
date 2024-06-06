namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class UltrasonicSensor
    {
        public int PinTrig { get; set; } = 12;
        public int PinEcho { get; set; } = 15;
        public float GetDistance() { return 0.0f; }
        public void Setup() { }
    }
}
