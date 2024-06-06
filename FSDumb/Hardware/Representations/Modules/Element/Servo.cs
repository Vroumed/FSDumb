namespace Vroumed.FSDumb.Hardware.Representations.Modules.Element
{
    public class Servo
    {
        public int Pin { get; set; }
        public void SetAngle(int angle) { }
        public void Sweep(int startAngle, int endAngle) { }
    }
}
