using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRecorder.Model
{
    public class SettingsRecordModel : IDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public int Databits { get; set; }
        public int Parity { get; set; }
        public int Stopbits { get; set; }
        public int Period { get; set; }
    }
}
