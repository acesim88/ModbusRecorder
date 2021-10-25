using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRecorder
{
    public interface IRestService
    {
        Task<T> GetData<T>(string url, string requestString);

    }
}
