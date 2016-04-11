using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class RobotHMIViewModel : ViewModelBase
    {
        private static event EventHandler RefreshGuiTimerElapsed;
        private static bool VMConnected;
        private static bool OnRefreshGuiTimerElapsed()
        {
            RefreshGuiTimerElapsed?.Invoke(null, null);
            return VMConnected;
        }

        Robot _robot;

        public RobotHMIViewModel(Robot robot)
        {
            _robot = robot;

            SetDirectionCommand = new Command(
                para => _robot.SetDirection_Async((RobotDirection)para)
            );

            //TODO: This timer pattern is pretty sketchy. Need to use the 
            //static event to properly clean things up on disconnect.
            RefreshGuiTimerElapsed += RobotHMIViewModel_RefreshGuiTimerElapsed;

            VMConnected = true;
            Device.StartTimer(Constants.GuiUpdateInterval, OnRefreshGuiTimerElapsed);
        }

        private async void RobotHMIViewModel_RefreshGuiTimerElapsed(object sender, EventArgs e)
        {
            DistanceToObstacle.Value = await _robot.GetDistanceToObstacle_Async();
        }

        public Command SetDirectionCommand { get; private set; }
        public NotifyingProperty<string> DistanceToObstacle { get; } = new NotifyingProperty<string>();

        public string Name { get { return _robot.Name; } }

        public override void UnregisterEvents()
        {
            VMConnected = false;
            RefreshGuiTimerElapsed -= RobotHMIViewModel_RefreshGuiTimerElapsed;
            //Allow the timer to be serviced before the socket is closed.
            //TODO: Find a better way to synchronize all of this.
            Task.Delay(Constants.GuiUpdateInterval);
        }
    }
}
