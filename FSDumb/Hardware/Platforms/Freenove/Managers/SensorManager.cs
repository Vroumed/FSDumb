using Vroumed.FSDumb.Hardware.Platforms.Freenove.Modules;
using Vroumed.FSDumb.Hardware.Representations.Modules;

namespace Vroumed.FSDumb.Hardware.Platforms.Freenove.Managers
{
    internal class SensorManager : ISensors
    {
        public Battery Battery { get; } = new Battery();
        public LightSensor LightSensor { get; } = new LightSensor();
        public UltrasonicSensor UltrasonicSensor { get; } = new UltrasonicSensor();
        public TrackSensor TrackSensor { get; } = new TrackSensor();
        
        public float GetBatteryVoltage()
        {
            return Battery.GetVoltage();
        }

        public int GetLightSensorLevel()
        {
            return LightSensor.GetLightLevel();
        }

        public float GetUltrasonicDistance()
        {
            return UltrasonicSensor.GetDistance();
        }

        public int GetTrackSensorData()
        {
            return TrackSensor.Read();
        }
    }
}
