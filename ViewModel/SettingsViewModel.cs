using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ModbusRecorder.Annotations;
using ModbusRecorder.Model;
using ModbusRecorder.Service;
using ModbusRecorder.Utils;

namespace ModbusRecorder.ViewModel
{
    public class SettingsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ISettingsRecordService _settingsRecordService;

        public UserCommand SaveRecordCommand { get; set; }

        private SettingsRecordModel _settings;

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

        public SettingsViewModel(ISettingsRecordService settingsRecordService)
        {
            _settingsRecordService = settingsRecordService;
            _settingsRecordService.SettingsRecordModelAdded += _settingsRecordService_SettingsRecordModelAdded;
            _settingsRecordService.SettingsRecordModelUpdated += _settingsRecordService_SettingsRecordModelUpdated;

            SaveRecordCommand = new UserCommand(OnSaveRecordCommand);

            Task.Run(() => Connections = new ObservableCollection<string>(SerialPort.GetPortNames()));

            Task.Run(GetSettings);
        }

        private void _settingsRecordService_SettingsRecordModelAdded(object sender, SettingsRecordModel e)
        {
            UpdateSettings(e);
        }

        private void _settingsRecordService_SettingsRecordModelUpdated(object sender, SettingsRecordModel e)
        {
            UpdateSettings(e);
        }

        private void UpdateSettings(SettingsRecordModel settingsRecordModel)
        {
            _settings = new SettingsRecordModel()
            {
                Id = settingsRecordModel.Id,
                Name = settingsRecordModel.Name,
                Baudrate = settingsRecordModel.Baudrate,
                Databits = settingsRecordModel.Databits,
                PortName = settingsRecordModel.PortName,
                Parity = settingsRecordModel.Parity,
                Period = settingsRecordModel.Period,
                Stopbits = settingsRecordModel.Stopbits
            };
        }

        private void OnSaveRecordCommand(object obj)
        {
            _settings.PortName = "COM10";
            _settings.Baudrate = 9600;
            _settingsRecordService.SaveSettingsRecord(_settings);
        }

        public async void GetSettings()
        {
            _settings = await _settingsRecordService.GetSettingsRecord();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override void OnDispose()
        {
            _settingsRecordService.SettingsRecordModelAdded -= _settingsRecordService_SettingsRecordModelAdded;
            _settingsRecordService.SettingsRecordModelUpdated -= _settingsRecordService_SettingsRecordModelUpdated;
            base.OnDispose();
        }
    }
}
