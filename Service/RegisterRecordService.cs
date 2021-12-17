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

        public event EventHandler<RegisterRecordModel> RegisterRecordModelAdded;
        public event EventHandler<RegisterRecordModel> RegisterRecordModelUpdated;
        public event EventHandler<RegisterRecordModel> RegisterRecordModelDeleted;

        public void AddRegisterRecord(RegisterRecordModel registerRecordModel)
        {
            if (registerRecordModel.Id != 0)
            {
                var register = _registerRecordModels.FirstOrDefault(x => x.Id == registerRecordModel.Id);
                if (register != null)
                {
                    register.DeviceAddress = registerRecordModel.DeviceAddress;
                    register.RegisterAddress = registerRecordModel.RegisterAddress;
                    register.RegisterType = registerRecordModel.RegisterType;
                    register.Name = registerRecordModel.Name;
                    register.RecordDescription = registerRecordModel.RecordDescription;
                    register.DownLimit = registerRecordModel.DownLimit;
                    register.UpLimit = registerRecordModel.UpLimit;
                    register.IsAlertActivated = registerRecordModel.IsAlertActivated;
                    
                    UpdateRecord(register);

                    RegisterRecordModelUpdated?.Invoke(this, register);
                }
            }
            else
            {
                _registerRecordModels.Add(registerRecordModel);
                AddRecord(registerRecordModel);

                RegisterRecordModelAdded?.Invoke(this, registerRecordModel);
            }
        }

        public async Task DeleteRegisterRecord(string recordName)
        {
            if (_registerRecordModels.Any(x => x.Name == recordName))
            {
                RegisterRecordModel registerRecordModel = _registerRecordModels.FirstOrDefault(x => x.Name == recordName);
                _registerRecordModels.Remove(registerRecordModel);
                await Task.Run(() => DeleteRecord(registerRecordModel));

                RegisterRecordModelDeleted?.Invoke(this, registerRecordModel);
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
