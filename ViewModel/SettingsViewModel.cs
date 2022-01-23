using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
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
        private readonly ISettingsRecordService _settingsRecordService;

        public UserCommand SaveRecordCommand { get; set; }

        private SettingsRecordModel _settings;

        private string _selectedConnection;
        private ObservableCollection<string> _connections;
        private int _selectedBaudrate;
        private ObservableCollection<int> _baudrates;
        private Parity _selectedParity;
        private ObservableCollection<Parity> _parity;
        private int _selectedDataBit;
        private ObservableCollection<int> _dataBits;
        private StopBits _selectedStopBit;
        private ObservableCollection<StopBits> _stopBits;
        private int _selectedPeriod;
        private ObservableCollection<int> _periods;

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

        public int SelectedBaudrate
        {
            get => _selectedBaudrate;
            set
            {
                if (value == _selectedBaudrate) return;
                _selectedBaudrate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> Baudrates
        {
            get => _baudrates;
            set
            {
                _baudrates = value;
                OnPropertyChanged();
            }
        }

        public int SelectedDataBit
        {
            get => _selectedDataBit;
            set
            {
                if (value == _selectedDataBit) return;
                _selectedDataBit = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> DataBits
        {
            get => _dataBits;
            set
            {
                _dataBits = value;
                OnPropertyChanged();
            }
        }

        public Parity SelectedParity
        {
            get => _selectedParity;
            set
            {
                if (value == _selectedParity) return;
                _selectedParity = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Parity> Parity
        {
            get => _parity;
            set
            {
                _parity = value;
                OnPropertyChanged();
            }
        }

        public StopBits SelectedStopBit
        {
            get => _selectedStopBit;
            set
            {
                if (value == _selectedStopBit) return;
                _selectedStopBit = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StopBits> StopBits
        {
            get => _stopBits;
            set
            {
                _stopBits = value;
                OnPropertyChanged();
            }
        }

        public int SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                if (value == _selectedPeriod) return;
                _selectedPeriod = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> Periods
        {
            get => _periods;
            set
            {
                _periods = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel(ISettingsRecordService settingsRecordService)
        {
            _settingsRecordService = settingsRecordService;
            _settingsRecordService.SettingsRecordModelAdded += _settingsRecordService_SettingsRecordModelAdded;
            _settingsRecordService.SettingsRecordModelUpdated += _settingsRecordService_SettingsRecordModelUpdated;

            SaveRecordCommand = new UserCommand(OnSaveRecordCommand);

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

        private async void OnSaveRecordCommand(object obj)
        {
            _settings.PortName = SelectedConnection;
            _settings.Baudrate = SelectedBaudrate;
            _settings.Databits = SelectedDataBit;
            _settings.Parity = (int)SelectedParity;
            _settings.Stopbits = (int)SelectedStopBit;
            _settings.Period = SelectedPeriod;

            await _settingsRecordService.SaveSettingsRecord(_settings);

            this.CloseCommand.Execute(null);
        }

        public async void GetSettings()
        {
            _settings = await _settingsRecordService.GetSettingsRecord();

            await Task.Run(() => Connections = new ObservableCollection<string>(SerialPort.GetPortNames()));

            Baudrates = new ObservableCollection<int>()
            {
                1200, 2400, 4800, 9600, 19200
            };

            DataBits = new ObservableCollection<int>()
            {
                7,8
            };

            Parity = new ObservableCollection<Parity>()
            {
                System.IO.Ports.Parity.Even,
                System.IO.Ports.Parity.Mark,
                System.IO.Ports.Parity.None,
                System.IO.Ports.Parity.Odd,
                System.IO.Ports.Parity.Space
            };

            StopBits = new ObservableCollection<StopBits>()
            {
                System.IO.Ports.StopBits.None,
                System.IO.Ports.StopBits.One,
                System.IO.Ports.StopBits.OnePointFive,
                System.IO.Ports.StopBits.Two,
            };

            Periods = new ObservableCollection<int>()
            {
                1, 5, 10, 15, 30, 60
            };

            if (_settings.Id!=0)
            {
                SelectedBaudrate = _settings.Baudrate;
                SelectedDataBit = _settings.Databits;
                SelectedParity = (Parity)_settings.Parity;
                SelectedStopBit = (StopBits)_settings.Stopbits;
                SelectedPeriod = _settings.Period;
            }
            else
            {
                SelectedBaudrate = Baudrates.FirstOrDefault(x => x == 9600);
                SelectedDataBit = DataBits.FirstOrDefault(x => x == 8);
                SelectedParity = Parity.FirstOrDefault(x => x == System.IO.Ports.Parity.None);
                SelectedStopBit = StopBits.FirstOrDefault(x => x == System.IO.Ports.StopBits.One);
                SelectedPeriod = Periods.FirstOrDefault(x => x == 1);
            }
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
