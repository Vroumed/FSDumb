using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Hardware.Platforms.Common.Modules;
using Vroumed.FSDumb.Hardware.Platforms.RWD.Modules;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.RWD
{
    public class RWDHardwareAccessor : IHardwareAccessor, IDependencyCandidate
    {
        public RWDHardwareAccessor(DependencyInjector injector)
        {
            Lighting = injector.Resolve<LightingManager>();
        }

        public IBuzzer Buzzer { get; } = new Buzzer();
        public ILighting Lighting { get; }
        public IMovement Movement { get; }
        public ISensors Sensors { get; }
        public IVision Vision { get; }
    }
}
