using System;
using System.Collections.Generic;
using System.Text;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface IMovement
    {
        public float Speed { get; set; }

        public float Angle { get; set; }
    }
}
