using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class MainViewModel : ViewModelBase
    {
        IBluetoothDeviceLister _deviceLister;
        IBluetoothDevice _currentDevice;

        public MainViewModel()
        {
            _deviceLister = DependencyService.Get<IBluetoothDeviceLister>();

            ActiveViewModel.Value = new DeviceListingViewModel(_deviceLister, Connect);
        }

        public NotifyingProperty<ViewModelBase> ActiveViewModel { get; } = new NotifyingProperty<ViewModelBase>();
        public NotifyingProperty<bool> Connecting { get; } = new NotifyingProperty<bool>();

        private async void Connect(IBluetoothDevice device)
        {
            _currentDevice = device;
            if(await Connect_Async()) ActiveViewModel.Value = new RobotHMIViewModel(new Robot(_currentDevice));
        }

        public override void UnregisterEvents()
        {
            ActiveViewModel.Value?.UnregisterEvents();
            _currentDevice?.Disconnect();
        }

        private async Task<bool> Connect_Async()
        {
            Connecting.Value = true;
            bool connected = await _currentDevice.Connect_Async();
            Connecting.Value = false;
            return connected;
        }
    }
}
