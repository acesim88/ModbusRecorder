using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRecorder.Model
{
    public class ReportModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastRecordTime { get; set; }
        public int LastRecord { get; set; }
        public int Period { get; set; }
        public int TotalRecord { get; set; }
        public decimal Average { get; set; }


    }
}
