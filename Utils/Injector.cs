using DryIoc;
using ModbusRecorder.Service;

namespace ModbusRecorder.Utils
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
            _container.Register<ISettingsRecordService, SettingsRecordService>(Reuse.Singleton);
        }
        public static TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }
    }
}
