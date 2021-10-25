using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ModbusRecorder.Annotations;
using ModbusRecorder.Service;

namespace ModbusRecorder.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IRestService _restService;
        private IDbRecordService _dbRecordService;

        public MainWindowViewModel(IRestService restService, IDbRecordService dbRecordService)
        {
            _restService = restService;
            _dbRecordService = dbRecordService;

            RegisterList = new ObservableCollection<Register>();

            for (int i = 0; i < 10; i++)
            {
                RegisterList.Add(new Register()
                {
                    DeviceAdress = i,
                    RegisterAddress = i * 5,
                    Name = i.ToString(),
                    Description = "asdasda"
                });
            }

            _demoItemsView = CollectionViewSource.GetDefaultView(RegisterList);
            _demoItemsView.Filter = DemoItemsFilter;
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

        private readonly ICollectionView _demoItemsView;
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
    }
}
