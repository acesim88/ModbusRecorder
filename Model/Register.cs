using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Annotations;

namespace ModbusRecorder.Model
{
    public class Register:INotifyPropertyChanged
    {
        public int Id { get; set; }
        private int _deviceAdress;
        private int _registerAdress;
        private string _registerType;
        private string _name;
        private string _description;
        private double _downLimit;
        private double _upLimit;
        private bool _isAlertActivated;

        public int DeviceAdress
        {
            get => _deviceAdress;
            set
            {
                _deviceAdress = value;
                OnPropertyChanged();
            }
        }

        public int RegisterAddress
        {
            get => _registerAdress;
            set
            {
                _registerAdress = value;
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
