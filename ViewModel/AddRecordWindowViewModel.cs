using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ModbusRecorder.Annotations;

namespace ModbusRecorder.ViewModel
{
    public class AddRecordWindowViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public UserCommand AddRecordCommand { get; set; }

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
            AddRecordCommand = new UserCommand(OnAddRecordCommand);
        }

        private void OnAddRecordCommand(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
