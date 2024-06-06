using Vroumed.FSDumb.Hardware.Representations.Modules;
namespace Vroumed.FSDumb.Hardware.Representations
{
    public interface IHardwareAccessor
    {
        public IBuzzer Buzzer { get; }
        public ILighting Lighting { get; }
        public IMovement Movement { get; }
        public ISensors Sensors { get; }
        public IVision? Vision { get; }

    }
}
