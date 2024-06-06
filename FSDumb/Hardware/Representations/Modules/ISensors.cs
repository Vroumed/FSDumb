using Vroumed.FSDumb.Hardware.Representations.Modules.Sensors;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface ISensors
    {
        public float GetBatteryVoltage();
        public int GetLightSensorLevel();
        public float GetUltrasonicDistance();
        public int GetTrackSensorData();
    }
}
