using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotClientXamarin
{
    public enum RobotDirection
    {
        Forward,
        Backward,
        TurnLeft,
        TurnRight,
        Override,
        Stop
    }

    public class Robot
    {
        //using the toString for these,
        //lower-case to mantain C++ standard
        //on the server side
        private enum RobotProperty
        {
            direction,
            leftMotorForward,
            rightMotorForward,
            leftMotorBackward,
            rightMotorBackward,
            leftMotorOverride,
            rightMotorOverride,
            distance
        }

        IBluetoothDevice _bluetoothDevice;
        public Robot(IBluetoothDevice bluetoothDevice)
        {
            _bluetoothDevice = bluetoothDevice;
        }

        public string Name { get { return _bluetoothDevice.Name; } }

        public async void SetDirection_Async(RobotDirection direction)
        {
            await SetProperty_Async(RobotProperty.direction, direction);
        }

        public string GetDistanceToObstacle()
        {
            return GetProperty(RobotProperty.distance);
        }

        private async Task SetProperty_Async(RobotProperty property, object value)
        {
            await _bluetoothDevice.Write_Async(string.Format(Constants.SetPropertyFmt, property.ToString(), value.ToString()));
        }

        private async Task<string> GetProperty_Async(RobotProperty property)
        {
            return await _bluetoothDevice.Transact_Async(string.Format(Constants.GetPropertyFmt, property.ToString()));
        }

        private string GetProperty(RobotProperty property)
        {
            return _bluetoothDevice.Transact(string.Format(Constants.GetPropertyFmt, property.ToString()));
        }
    }
}
