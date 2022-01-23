using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using ModbusRecorder.Utils;
using ModbusRecorder.ViewModel;

namespace ModbusRecorder.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = Injector.GetInstance<MainWindowViewModel>();

            DataContext = viewModel;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ((MainWindowViewModel)DataContext)?.GetBtc();
                ((MainWindowViewModel)DataContext)?.GetRegisters();
                ((MainWindowViewModel)DataContext)?.GetSettings();
            });
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
            => DemoItemsSearchBox.Focus();

        private void AddRecordPopupButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddRecordWindow view = new AddRecordWindow();
            var viewNodel = Injector.GetInstance<AddRecordWindowViewModel>();
            viewNodel.Title = "Yeni Kayıt";
            view.DataContext = viewNodel;
            viewNodel.Init(view);
            view.Show();
        }

        private void ReportsButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddRecordWindow view = new AddRecordWindow();
            var viewNodel = Injector.GetInstance<AddRecordWindowViewModel>();
            viewNodel.Title = "Yeni Kayıt";
            view.DataContext = viewNodel;
            viewNodel.Init(view);
            view.Show();
        }

        private void ClosePopupButtonOnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsButtonOnClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow view = new SettingsWindow();
            var viewNodel = Injector.GetInstance<SettingsViewModel>();
            view.DataContext = viewNodel;
            viewNodel.Init(view);
            view.Show();
        }

        private void ConnectButtonOnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/atakancesim/");
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    public class Time
    {
        public string updated { get; set; }
        public DateTime updatedISO { get; set; }
        public string updateduk { get; set; }
    }

    public class USD
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }

    public class GBP
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }

    public class EUR
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }

    public class Bpi
    {
        public USD USD { get; set; }
        public GBP GBP { get; set; }
        public EUR EUR { get; set; }
    }

    public class BtcPrice
    {
        public Time time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Bpi bpi { get; set; }
    }

}
