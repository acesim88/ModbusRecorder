using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using Example;
using ModbusRecorder.Service;

namespace ModbusRecorder
{
    public static class Injector
    {
        private static Container _container;

        public static void InitializeReferences()
        {
            _container = new Container(Rules.Default.WithConcreteTypeDynamicRegistrations().WithoutThrowOnRegisteringDisposableTransient());

            _container.Register<IDbRecordService, DbRecordService>(Reuse.Singleton);
            _container.Register<IModbusConnectionService, ModbusConnectionService>(Reuse.Singleton);
            _container.Register<IRestService, RestService>(Reuse.Singleton);
            _container.Register<IRegisterRecordService, RegisterRecordService>(Reuse.Singleton);
        }
        public static TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }
    }
}
