using System.Collections.Generic;

namespace ModbusRecorder.Model
{
    public class RegisterRecordModel:IDbModel
    {
        public string Name { get; set; }
        public int DeviceAddress { get; set; }
        public int RegisterAddress{ get; set; }
        public string RegisterType { get; set; }        
        public string RecordDescription{ get; set; }
        public double DownLimit{ get; set; }
        public double UpLimit{ get; set; }
        public bool IsAlertActivated{ get; set; }
    }
}
