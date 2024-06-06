using Vroumed.FSDumb.Hardware.Representations.Modules.Element;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public class Wheels
    {
        public Wheel TopLeft { get; } = new Wheel { ForwardPin = 26, BackwardPin = 27 };
        public Wheel TopRight { get; } = new Wheel { ForwardPin = 14, BackwardPin = 12 };
        public Wheel BottomLeft { get; } = new Wheel { ForwardPin = 13, BackwardPin = 15 };
        public Wheel BottomRight { get; } = new Wheel { ForwardPin = 32, BackwardPin = 33 };

    }
}
