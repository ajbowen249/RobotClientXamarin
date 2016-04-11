using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RobotClientXamarin.Droid;
using Android.Bluetooth;
using Java.Util;
using System.ComponentModel;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothDevice_Android))]
namespace RobotClientXamarin.Droid
{
    public class BluetoothDevice_Android : IBluetoothDevice
    {
        BluetoothDevice _device;
        bool _connected;
        UUID _socketID;
        BluetoothSocket _socket;
        object _ioLock = new object();

        public BluetoothDevice_Android(BluetoothDevice device)
        {
            _device = device;
            _socketID = UUID.FromString(Constants.GenericBluetoothSerialDeviceUUID);
        }

        public string Name
        {
            get { return _device.Name; }
        }

        public async Task<bool> Connect_Async()
        {
            try
            {
                _socket = _device.CreateRfcommSocketToServiceRecord(_socketID);
                await _socket.ConnectAsync();
                _connected = true;
            }
            catch { }

            return _connected;
        }

        public void Disconnect()
        {
            lock(_ioLock)
            {
                _socket.Close();
            }
        }

        public Task<string> Transact_Async(string request)
        {
            return Task.Run(() =>
            {
                return Transact(request);
            });
        }

        public string Transact(string request)
        {
            lock (_ioLock)
            {
                _socket.OutputStream.Write(ASCIIEncoding.ASCII.GetBytes(request), 0, request.Length);

                var recieveBuffer = "";

                while (!recieveBuffer.EndsWith("\n"))
                    recieveBuffer += (char)_socket.InputStream.ReadByte();

                return recieveBuffer.Replace("\n", "");
            }
        }

        public async Task Write_Async(string content)
        {
            await Task.Run(() =>
            {
                lock (_ioLock)
                {
                    _socket.OutputStream.Write(ASCIIEncoding.ASCII.GetBytes(content), 0, content.Length);
                }
            });
        }
    }
}