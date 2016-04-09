using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotClientXamarin
{
    public interface IBluetoothDeviceLister
    {
        IEnumerable<IBluetoothDevice> GetPairedDevices();
    }
}
