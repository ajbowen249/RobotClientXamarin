using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class VVMControl : ContentView
    {
        static readonly Dictionary<Type, Func<View>> _associations = new Dictionary<Type, Func<View>>() {
            { typeof(DeviceListingViewModel), () => new DeviceListingView() },
            { typeof(RobotHMIViewModel), () => new RobotHMIView() }
        };

        public static BindableProperty ViewModelProperty =
            BindableProperty.Create<VVMControl, ViewModelBase>(
                ctrl => ctrl.ViewModel,
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: ViewModelChanged
            );

        public ViewModelBase ViewModel
        {
            get { return (ViewModelBase)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static void ViewModelChanged(BindableObject bindable, ViewModelBase oldValue, ViewModelBase newValue)
        {
            var specificBindable = (VVMControl)bindable;
            var view = _associations[newValue.GetType()]();
            view.BindingContext = newValue;
            specificBindable.Content = view;
        }
    }
}
