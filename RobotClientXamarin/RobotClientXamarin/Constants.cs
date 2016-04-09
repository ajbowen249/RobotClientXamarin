using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotClientXamarin
{
    public static class Constants
    {
        public const char ClearBuffer = '#';
        public static readonly string SetPropertyFmt = $"{ClearBuffer}{{0}}={{1}};";
        public static readonly string GetPropertyFmt = $"{ClearBuffer}{{0}}?";
        public static readonly string GenericBluetoothSerialDeviceUUID = "00001101-0000-1000-8000-00805F9B34FB";
        public static readonly TimeSpan GuiUpdateInterval = TimeSpan.FromMilliseconds(150);
    }
}
