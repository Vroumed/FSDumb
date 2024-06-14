using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Hardware.Modules;
using Vroumed.FSDumb.Hardware.Platforms.Freenove.Managers;
using Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove
{
    public class FreenoveHardware : IHardwareAccessor, IDependencyCandidate
    {
        public FreenoveHardware(DependencyInjector injector)
        {
            Lighting = injector.Resolve<LEDs>();
        }

        public IBuzzer Buzzer { get; } = new Buzzer();
        public ILighting Lighting { get; }
        public IMovement Movement { get; } = new MovementManager();
        public ISensors Sensors { get; } = new SensorManager();
        public IVision Vision { get; } = new VisionManager();
    }
}
