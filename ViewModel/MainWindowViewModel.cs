using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ModbusRecorder.Annotations;
using ModbusRecorder.Model;
using ModbusRecorder.Service;
using ModbusRecorder.Utils;
using ModbusRecorder.View;

namespace ModbusRecorder.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IRestService _restService;
        private IRegisterRecordService _registerRecordService;
        private ISettingsRecordService _settingsRecordService;

        public UserCommand DeleteRecordCommand { get; set; }
        public UserCommand OpenRecordSettingsCommand { get; set; }

        public MainWindowViewModel(IRestService restService, IRegisterRecordService registerRecordService, ISettingsRecordService settingsRecordService)
        {
            _restService = restService;
            _registerRecordService = registerRecordService;
            _settingsRecordService = settingsRecordService;

            _registerRecordService.RegisterRecordModelAdded += _registerRecordService_RegisterRecordModelAdded;
            _registerRecordService.RegisterRecordModelUpdated += _registerRecordService_RegisterRecordModelUpdated;
            _registerRecordService.RegisterRecordModelDeleted += _registerRecordService_RegisterRecordModelDeleted;

            _settingsRecordService.SettingsRecordModelAdded += _settingsRecordService_SettingsRecordModelAdded;
            _settingsRecordService.SettingsRecordModelUpdated += _settingsRecordService_SettingsRecordModelUpdated;

            OpenRecordSettingsCommand = new UserCommand(OnOpenRecordSettingsCommand);
            DeleteRecordCommand = new UserCommand(OnDeleteRecordCommand);
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

        private void _registerRecordService_RegisterRecordModelAdded(object sender, RegisterRecordModel e)
        {
            RegisterList.Add(new Register()
            {
                Id = e.Id,
                DeviceAdress = e.DeviceAddress,
                RegisterAddress = e.RegisterAddress,
                RegisterType = e.RegisterType,
                Name = e.Name,
                Description = e.RecordDescription,
                DownLimit = e.DownLimit,
                UpLimit = e.UpLimit,
                IsAlertActivated = e.IsAlertActivated
            });
        }

        private void _registerRecordService_RegisterRecordModelUpdated(object sender, RegisterRecordModel e)
        {
            var register = RegisterList.FirstOrDefault(x => x.Id == e.Id);

            if (register != null)
            {
                register.DeviceAdress = e.DeviceAddress;
                register.RegisterAddress = e.RegisterAddress;
                register.RegisterType = e.RegisterType;
                register.Name = e.Name;
                register.Description = e.RecordDescription;
                register.DownLimit = e.DownLimit;
                register.UpLimit = e.UpLimit;
                register.IsAlertActivated = e.IsAlertActivated;
            }
        }

        private void _registerRecordService_RegisterRecordModelDeleted(object sender, RegisterRecordModel e)
        {
            var register = RegisterList.FirstOrDefault(x => x.Id == e.Id);
            if (register != null)
            {
                RegisterList.Remove(register);
            }
        }

        private void OnOpenRecordSettingsCommand(object obj)
        {
            if (obj is Register register)
            {
                AddRecordWindow view = new AddRecordWindow();
                var viewNodel = Injector.GetInstance<AddRecordWindowViewModel>();
                view.DataContext = viewNodel;

                viewNodel.Id = register.Id;
                viewNodel.DeviceAddress = register.DeviceAdress;
                viewNodel.RegisterAddress = register.RegisterAddress;
                viewNodel.RegisterType = register.RegisterType;
                viewNodel.RecordName = register.Name;
                viewNodel.RecordDescription = register.Description;
                viewNodel.DownLimit = register.DownLimit;
                viewNodel.UpLimit = register.UpLimit;
                viewNodel.IsAlertActivated = register.IsAlertActivated;
                viewNodel.IsUpdate = true;
                viewNodel.Title = "Kayıt Güncelleme";

                viewNodel.Init(view);
                view.Show();
            }
        }

        private void OnDeleteRecordCommand(object obj)
        {
            if (obj is Register register)
            {
                RegisterList.Remove(register);

                Task.Run(() => _registerRecordService.DeleteRegisterRecord(register.Name));
            }
        }

        private Register _selectedRegister;
        public Register SelectedRegister
        {
            get => _selectedRegister;
            set
            {
                _selectedRegister = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Register> _registerList;
        public ObservableCollection<Register> RegisterList
        {
            get => _registerList;
            set
            {
                _registerList = value;
                OnPropertyChanged();
            }
        }

        private ICollectionView _demoItemsView;
        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (_searchKeyword == value) return;

                _searchKeyword = value;
                _demoItemsView.Refresh();
            }
        }

        private bool DemoItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }

            return obj is Register item && item.Name.ToLower().Contains(_searchKeyword.ToLower());
        }

        private SettingsRecordModel _settings;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetBtc()
        {
            var tbcPrice = await _restService.GetData<BtcPrice>("https://api.coindesk.com", "v1/bpi/currentprice.json");
        }

        public async Task GetRegisters()
        {
            RegisterList = new ObservableCollection<Register>();

            var records = await _registerRecordService.GetRegisterRecords();

            foreach (var record in records)
            {
                RegisterList.Add(new Register()
                {
                    Id = record.Id,
                    Name = record.Name,
                    RegisterAddress = record.RegisterAddress,
                    RegisterType = record.RegisterType,
                    IsAlertActivated = record.IsAlertActivated,
                    Description = record.RecordDescription,
                    DeviceAdress = record.DeviceAddress,
                    DownLimit = record.DownLimit,
                    UpLimit = record.UpLimit,
                    AlertIconIsVisible = Visibility.Collapsed
                });
            }

            _demoItemsView = CollectionViewSource.GetDefaultView(RegisterList);

            _demoItemsView.Filter = DemoItemsFilter;
        }

        public async Task GetSettings()
        {
            _settings = await _settingsRecordService.GetSettingsRecord();
        }
    }
}
