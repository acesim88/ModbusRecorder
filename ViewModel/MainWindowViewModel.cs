using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using ModbusRecorder.Annotations;
using ModbusRecorder.Model;
using ModbusRecorder.Service;

namespace ModbusRecorder.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IRestService _restService;
        private IRegisterRecordService _registerRecordService;

        public MainWindowViewModel(IRestService restService, IRegisterRecordService registerRecordService)
        {
            _restService = restService;
            _registerRecordService = registerRecordService;
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
                 Name = record.Name,
                 RegisterAddress = record.RegisterAddress,
                 RegisterType = record.RegisterType,
                 IsAlertActivated = record.IsAlertActivated,
                 Description = record.RecordDescription,
                 DeviceAdress = record.DeviceAddress,
                 DownLimit = record.DownLimit,
                 UpLimit = record.UpLimit,
             });   
            }

            _demoItemsView = CollectionViewSource.GetDefaultView(RegisterList);

            _demoItemsView.Filter = DemoItemsFilter;
        }
    }
}
