using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class App : Application
    {
        MainViewModel _mainView;

        public App()
        {
            // The root page of your application
            MainPage = new MainView();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            _mainView?.UnregisterEvents();
        }

        protected override void OnResume()
        {
            _mainView = new MainViewModel();
            BindingContext = _mainView;
        }
    }
}
