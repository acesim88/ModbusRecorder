using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModbusRecorder.Annotations;

namespace ModbusRecorder.ViewModel
{
    public class AddRecordWindowViewModel:INotifyPropertyChanged
    {

        private bool _isAlertActivated;

        public bool IsAlertActivated
        {
            get => _isAlertActivated;
            set
            {
                _isAlertActivated = value;
                OnPropertyChanged();
            }
        }

        public AddRecordWindowViewModel()
        {
            IsAlertActivated = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
