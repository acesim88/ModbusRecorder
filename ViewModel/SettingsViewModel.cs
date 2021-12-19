using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ModbusRecorder.Annotations;
using ModbusRecorder.Utils;

namespace ModbusRecorder.ViewModel
{
    public class SettingsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private string _selectedConnection;
        private ObservableCollection<string> _connections;

        public string SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                if (value == _selectedConnection) return;
                _selectedConnection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Connections
        {
            get => _connections;
            set
            {
                _connections = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            Task.Run(() => Connections = new ObservableCollection<string>(SerialPort.GetPortNames()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
