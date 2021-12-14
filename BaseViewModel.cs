using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModbusRecorder
{
    public class BaseViewModel
    {
        private Window _window = null;

        public UserCommand CloseCommand { get; set; }

        public void Init(Window window)
        {
            CloseCommand = new UserCommand(OnCloseCommand);

            _window = window;
        }

        private void OnCloseCommand(object obj)
        {
            _window.Close();
        }
    }
}
