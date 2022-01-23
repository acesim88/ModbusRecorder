using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Model;

namespace ModbusRecorder.Service
{
    public class SettingsRecordService : DbRecordService, ISettingsRecordService
    {

        private SettingsRecordModel _settingsRecordModel;

        public SettingsRecordService() : base("SettingsRecord")
        {

        }

        public event EventHandler<SettingsRecordModel> SettingsRecordModelAdded;
        public event EventHandler<SettingsRecordModel> SettingsRecordModelUpdated;

        public async Task SaveSettingsRecord(SettingsRecordModel settingsRecordModel)
        {
            if (settingsRecordModel.Id != 0)
            {
                UpdateRecord(settingsRecordModel);
                await GetSettingsRecord();

                SettingsRecordModelUpdated?.Invoke(this, _settingsRecordModel);
            }
            else
            {
                AddRecord(settingsRecordModel);
                await GetSettingsRecord();

                SettingsRecordModelAdded?.Invoke(this, _settingsRecordModel);
            }
        }

        public async Task<SettingsRecordModel> GetSettingsRecord()
        {
            _settingsRecordModel = new SettingsRecordModel();

            var records = await Task.Run(GetRecords);

            if (records != null && records.Count != 0)
            {
                var record = records.First();

                _settingsRecordModel = new SettingsRecordModel()
                {
                    Id = record.Id,
                    Name = record.Name,
                    PortName = ((SettingsRecordModel)record).PortName,
                    Baudrate = ((SettingsRecordModel)record).Baudrate,
                    Databits = ((SettingsRecordModel)record).Databits,
                    Parity = ((SettingsRecordModel)record).Parity,
                    Stopbits = ((SettingsRecordModel)record).Stopbits,
                    Period = ((SettingsRecordModel)record).Period
                };
            }

            return _settingsRecordModel;
        }
    }
}
