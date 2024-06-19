//This is a t4 file meant to be as Configuration template file, you must generate it by saving it if you are in Visual Studio
using System;

namespace Vroumed.FSDumb
{
    public static class RoverConfiguration
    {
        public enum Hardware 
        {
            AWD,
            RWD
        }

        public static string WifiSSID { get; } = "I Have A Big Engine";
        public static string Password { get; } = "LMAO no";
        public static string RoverKey { get; } = "potato";
        public static RoverConfiguration.Hardware HardwareType { get; } = Hardware.RWD;
    }
}
