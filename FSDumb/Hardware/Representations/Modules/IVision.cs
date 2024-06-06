using System;
using System.Collections.Generic;
using System.Text;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface IVision
    {
        public byte[] GetImage();

        public void MoveOrientation(float x, float y);
    }
}
