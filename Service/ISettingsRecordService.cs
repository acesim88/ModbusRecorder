using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Model;

namespace ModbusRecorder.Service
{
    public interface ISettingsRecordService
    {
        event EventHandler<SettingsRecordModel> SettingsRecordModelAdded;
        event EventHandler<SettingsRecordModel> SettingsRecordModelUpdated;

        Task SaveSettingsRecord(SettingsRecordModel settingsRecordService);
        Task<SettingsRecordModel> GetSettingsRecord();
    }
}
