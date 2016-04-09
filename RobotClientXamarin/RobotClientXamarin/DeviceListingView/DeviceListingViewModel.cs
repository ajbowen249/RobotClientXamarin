using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class DeviceListingViewModel : ViewModelBase
    {
        IBluetoothDeviceLister _deviceLister;
        public DeviceListingViewModel(IBluetoothDeviceLister deviceLister, Action<IBluetoothDevice> connectCallback)
        {
            _deviceLister = deviceLister;

            ConnectCommand = new Command(
                para => connectCallback(SelectedDevice.Value),
                canExecute => SelectedDevice.Value != null
            );

            SelectedDevice = new NotifyingProperty<IBluetoothDevice>(dependentCommand: ConnectCommand);

            foreach (var device in _deviceLister.GetPairedDevices())
                PairedDevices.Add(device);
        }

        public NotifyingProperty<IBluetoothDevice> SelectedDevice { get; }
        public ObservableCollection<IBluetoothDevice> PairedDevices { get; } = new ObservableCollection<IBluetoothDevice>();
        public Command ConnectCommand { get; private set; }
    }
}
