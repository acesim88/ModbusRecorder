using System.Collections.Generic;
using System.Windows.Documents;
using ModbusRecorder.Model;
using Newtonsoft.Json.Bson;

namespace ModbusRecorder.Service
{
    public interface IDbRecordService
    {
        bool AddRecord(IDbModel dbModel);
        void DeleteRecord(IDbModel dbModel);
        bool ReadRecord(IDbModel dbModel);
        List<IDbModel> GetRecords();
    }
}
