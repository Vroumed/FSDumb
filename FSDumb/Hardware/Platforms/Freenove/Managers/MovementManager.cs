using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Managers
{
    internal class MovementManager : IMovement
    {
        private float _speed = 0;
        private float _angle = 0;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                Invalidate();
            }
        }

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Invalidate the current pin configurations
        /// </summary>
        public void Invalidate()
        {

        }
    }
}
