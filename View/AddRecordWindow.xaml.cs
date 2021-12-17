using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModbusRecorder.View
{
    /// <summary>
    /// Interaction logic for AddRecordsWindow.xaml
    /// </summary>
    public partial class AddRecordWindow : Window
    {
        public AddRecordWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            double val;

            e.Handled = !double.TryParse(fullText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out val);
        }
    }
}
