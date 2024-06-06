namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class Battery
    {
        public int Pin { get; set; } = 34;
        public float Coefficient { get; set; } = 3.7f;
        public float GetVoltage() { return 0.0f; }
    }
}
