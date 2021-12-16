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
        bool AddRegisterRecord(RegisterRecordModel registerRecordModel);
        bool DeleteRegisterRecord(RegisterRecordModel registerRecordModel);
        RegisterRecordModel ReadRegisterRecord(string recordName);
        Task<List<RegisterRecordModel>> GetRegisterRecords();
    }
}
