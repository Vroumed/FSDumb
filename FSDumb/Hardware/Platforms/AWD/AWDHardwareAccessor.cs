using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Hardware.Platforms.AWD.Managers;
using Vroumed.FSDumb.Hardware.Platforms.AWD.Modules;
using Vroumed.FSDumb.Hardware.Platforms.Common.Modules;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.AWD
{
    public class AWDHardwareAccessor : IHardwareAccessor, IDependencyCandidate
    {
        public AWDHardwareAccessor(DependencyInjector injector)
        {
            Lighting = injector.Resolve<LightingManager>();
        }

        public IBuzzer Buzzer { get; } = new Buzzer();
        public ILighting Lighting { get; }
        public IMovement Movement { get; } = new MovementManager();
        public ISensors Sensors { get; } = new SensorManager();
        public IVision Vision { get; } = new VisionManager();
    }
}
