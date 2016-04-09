using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RobotClientXamarin.Droid
{
    [Activity(Label = "RobotClientXamarin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Device-specific implementations
            Xamarin.Forms.DependencyService.Register<BluetoothDeviceLister_Android>();
            Xamarin.Forms.DependencyService.Register<BluetoothDevice_Android>();

            LoadApplication(new App());
        }
    }
}

