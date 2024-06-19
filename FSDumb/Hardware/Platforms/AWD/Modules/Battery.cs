namespace Vroumed.FSDumb.Hardware.Platforms.AWD.Modules
{
    public class Battery
    {
        public int Pin { get; set; } = 34;
        public float Coefficient { get; set; } = 3.7f;
        public float GetVoltage() { return 0.0f; }
    }
}
