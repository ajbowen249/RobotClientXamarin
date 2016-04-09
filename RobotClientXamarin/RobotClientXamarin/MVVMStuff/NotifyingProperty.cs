using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RobotClientXamarin
{
    public class NotifyingProperty<T> : ViewModelBase
    {
        Command _dependentCommand;

        public NotifyingProperty(T initialValue = default(T), Command dependentCommand = null)
        {
            _value = initialValue;
            _dependentCommand = dependentCommand;
        }

        public T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                if ((value == null) ? _value != null : !value.Equals(_value))
                {
                    _value = value;
                    OnPropertyChanged("Value");
                    _dependentCommand?.ChangeCanExecute();
                }
            }
        }
    }
}
