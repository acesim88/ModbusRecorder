using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Model;

namespace ModbusRecorder.Service
{
    public interface IRegisterRecordService
    {
        event EventHandler<RegisterRecordModel> RegisterRecordModelAdded;
        event EventHandler<RegisterRecordModel> RegisterRecordModelUpdated;
        event EventHandler<RegisterRecordModel> RegisterRecordModelDeleted;

        void AddRegisterRecord(RegisterRecordModel registerRecordModel);
        Task DeleteRegisterRecord(string recordName);
        RegisterRecordModel ReadRegisterRecord(string recordName);
        Task<List<RegisterRecordModel>> GetRegisterRecords();
    }
}
