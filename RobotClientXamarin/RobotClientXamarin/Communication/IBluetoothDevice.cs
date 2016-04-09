using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotClientXamarin
{
    public interface IBluetoothDevice
    {
        string Name { get; }

        Task<bool> Connect_Async();
        Task Write_Async(string content);
        string Transact(string request);
        Task<string> Transact_Async(string request);
        void Disconnect();
    }
}
