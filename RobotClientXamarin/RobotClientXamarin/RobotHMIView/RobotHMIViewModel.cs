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
        Robot _robot;
        bool _unregistered;

        public RobotHMIViewModel(Robot robot)
        {
            _robot = robot;

            SetDirectionCommand = new Command(
                para => _robot.SetDirection_Async((RobotDirection)para)
            );

            //TODO: This timer is useless. It's entirely synchronous
            //and I'm not sure how to stop it before this VM gets cleaned up,
            //which is the sourc of the exception when the app closes
            Device.StartTimer(Constants.GuiUpdateInterval, UpdateGui);
        }

        public Command SetDirectionCommand { get; private set; }
        public NotifyingProperty<string> DistanceToObstacle { get; } = new NotifyingProperty<string>();

        public string Name { get { return _robot.Name; } }

        private bool UpdateGui()
        {
            DistanceToObstacle.Value = _robot.GetDistanceToObstacle();

            return !_unregistered;
        }

        public override void UnregisterEvents()
        {
            _unregistered = true;
        }
    }
}
