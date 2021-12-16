using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Model;

namespace ModbusRecorder.Service
{
    public class RegisterRecordService : DbRecordService, IRegisterRecordService
    {
        private List<RegisterRecordModel> _registerRecordModels;

        public RegisterRecordService() : base("RegisterRecords")
        {

        }

        public bool AddRegisterRecord(RegisterRecordModel registerRecordModel)
        {
            if (_registerRecordModels.Any(x => x.Name == registerRecordModel.Name))
            {
                return false;
            }
            else
            {
                _registerRecordModels.Add(registerRecordModel);
                return AddRecord(registerRecordModel);
            }
        }

        public bool DeleteRegisterRecord(RegisterRecordModel registerRecordModel)
        {
            if (_registerRecordModels.Any(x => x.Name == registerRecordModel.Name))
            {
                _registerRecordModels.Remove(_registerRecordModels.FirstOrDefault(x => x.Name == registerRecordModel.Name));
                return DeleteRecord(registerRecordModel);
            }
            else
            {
                return false;
            }
        }

        public RegisterRecordModel ReadRegisterRecord(string recordName)
        {
            return _registerRecordModels.FirstOrDefault(x => x.Name == recordName);
        }

        public async Task<List<RegisterRecordModel>> GetRegisterRecords()
        {
            if (_registerRecordModels == null)
            {
                _registerRecordModels = new List<RegisterRecordModel>();

                var records = await Task.Run(GetRecords);

                foreach (var record in records)
                {
                    _registerRecordModels.Add((RegisterRecordModel)record);
                }
            }

            return _registerRecordModels;
        }
    }
}
