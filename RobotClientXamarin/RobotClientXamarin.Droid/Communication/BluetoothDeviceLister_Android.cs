using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RobotClientXamarin.Droid;
using Android.Bluetooth;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothDeviceLister_Android))]
namespace RobotClientXamarin.Droid
{
    public class BluetoothDeviceLister_Android : IBluetoothDeviceLister
    {
        public IEnumerable<IBluetoothDevice> GetPairedDevices()
        {
            var adapter = BluetoothAdapter.DefaultAdapter;

            //todo: ask user
            if(!adapter.IsEnabled) adapter.Enable();

            return adapter.BondedDevices.Select(x => new BluetoothDevice_Android(x));
        }
    }
}