namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules
{
    public class Battery
    {
        public int Pin { get; set; } = 34;
        public float Coefficient { get; set; } = 3.7f;
        public float GetVoltage() { return 0.0f; }
    }
}
