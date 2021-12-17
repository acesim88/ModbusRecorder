using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModbusRecorder.Annotations;
using ModbusRecorder.Model;
using ModbusRecorder.Service;
using ModbusRecorder.Utils;

namespace ModbusRecorder.ViewModel
{
    public class AddRecordWindowViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IRegisterRecordService _registerRecordService;

        public UserCommand AddRecordCommand { get; set; }

        public bool IsUpdate = false;
        public int Id = 0;

        private int _deviceAddress;
        private int _registerAddress;
        private List<string> _registerTypes;
        private string _registerType;
        private string _recordName;
        private bool _isAlertActivated;
        private string _recordDescription;
        private double _downLimit;
        private double _upLimit;
        private string _recordStatus;
        private string _title;

        public int DeviceAddress
        {
            get => _deviceAddress;
            set
            {
                _deviceAddress = value;
                OnPropertyChanged();
            }
        }

        public int RegisterAddress
        {
            get => _registerAddress;
            set
            {
                _registerAddress = value;
                OnPropertyChanged();
            }
        }

        public List<string> RegisterTypes
        {
            get => _registerTypes;
            set
            {
                _registerTypes = value;
                OnPropertyChanged();
            }
        }

        public string RegisterType
        {
            get => _registerType;
            set
            {
                _registerType = value;
                OnPropertyChanged();
            }
        }

        public string RecordName
        {
            get => _recordName;
            set
            {
                _recordName = value;
                OnPropertyChanged();
            }
        }

        public string RecordDescription
        {
            get => _recordDescription;
            set
            {
                _recordDescription = value;
                OnPropertyChanged();
            }
        }

        public double DownLimit
        {
            get => _downLimit;
            set
            {
                _downLimit = value;
                OnPropertyChanged();
            }
        }

        public double UpLimit
        {
            get => _upLimit;
            set
            {
                _upLimit = value;
                OnPropertyChanged();
            }
        }

        public bool IsAlertActivated
        {
            get => _isAlertActivated;
            set
            {
                _isAlertActivated = value;
                OnPropertyChanged();
            }
        }

        public string RecordStatus
        {
            get => _recordStatus;
            set
            {
                _recordStatus = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public AddRecordWindowViewModel(IRegisterRecordService registerRecordService)
        {
            _registerRecordService = registerRecordService;

            AddRecordCommand = new UserCommand(OnAddRecordCommand);

            RegisterTypes = new List<string>()
            {
                "Coil", "Discrete Input", "Input Registers", "Holding Registers"
            };

            RecordStatus = string.Empty;
        }

        private void OnAddRecordCommand(object obj)
        {
            try
            {
                _registerRecordService.AddRegisterRecord(new RegisterRecordModel()
                {
                    Id = Id,
                    DeviceAddress = DeviceAddress,
                    RegisterAddress = RegisterAddress,
                    RegisterType = RegisterType,
                    Name = RecordName,
                    RecordDescription = RecordDescription,
                    DownLimit = DownLimit,
                    UpLimit = UpLimit,
                    IsAlertActivated = IsAlertActivated
                });

                RecordStatus = !IsUpdate ? "Kayıt yapıldı." : "Kayıt güncellendi.";
            }
            catch (Exception e)
            {
                RecordStatus = !IsUpdate ? "Kayıt yapılamadı." : "Kayıt güncellenemedi.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
