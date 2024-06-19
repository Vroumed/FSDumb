namespace Vroumed.FSDumb.Hardware.Platforms.AWD.Modules
{
    public class TrackSensor
    {
        public int Address { get; set; } = 0x20;
        public int PinSda { get; set; } = 13;
        public int PinScl { get; set; } = 14;
        public void Setup() { }
        public int Read() { return 0; }
    }
}
