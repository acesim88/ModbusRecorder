using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModbusRecorder.Annotations;
using ModbusRecorder.Model;
using ModbusRecorder.Utils;
using ModbusRecorder.View;

namespace ModbusRecorder.ViewModel
{
    public class ReportsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<ReportModel> _reportModels;

        public ObservableCollection<ReportModel> ReportModels
        {
            get => _reportModels;
            set
            {
                _reportModels = value;
                OnPropertyChanged();
            }
        }

        public UserCommand OpenReportCommand { get; set; }

        public ReportsViewModel()
        {
            ReportModels = new ObservableCollection<ReportModel>();

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 1",
                CreationDate=DateTime.Now.AddDays(-2),
                LastRecordTime=DateTime.Now,
                LastRecord=100,
                Period = 60,
                Average = 99,
                TotalRecord = 100
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 2",
                CreationDate=DateTime.Now.AddDays(-3),
                LastRecordTime=DateTime.Now,
                LastRecord=200,
                Period = 120,
                Average = 199,
                TotalRecord = 150
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 1",
                CreationDate=DateTime.Now.AddDays(-2),
                LastRecordTime=DateTime.Now,
                LastRecord=100,
                Period = 60,
                Average = 99,
                TotalRecord = 100
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 2",
                CreationDate=DateTime.Now.AddDays(-3),
                LastRecordTime=DateTime.Now,
                LastRecord=200,
                Period = 120,
                Average = 199,
                TotalRecord = 150
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 1",
                CreationDate=DateTime.Now.AddDays(-2),
                LastRecordTime=DateTime.Now,
                LastRecord=100,
                Period = 60,
                Average = 99,
                TotalRecord = 100
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 2",
                CreationDate=DateTime.Now.AddDays(-3),
                LastRecordTime=DateTime.Now,
                LastRecord=200,
                Period = 120,
                Average = 199,
                TotalRecord = 150
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 1",
                CreationDate=DateTime.Now.AddDays(-2),
                LastRecordTime=DateTime.Now,
                LastRecord=100,
                Period = 60,
                Average = 99,
                TotalRecord = 100
            });

            ReportModels.Add(new ReportModel()
            {
                Name="Kayıt 2",
                CreationDate=DateTime.Now.AddDays(-3),
                LastRecordTime=DateTime.Now,
                LastRecord=200,
                Period = 120,
                Average = 199,
                TotalRecord = 150
            });

            OpenReportCommand = new UserCommand(OnOpenReportCommand);
        }

        private void OnOpenReportCommand(object obj)
        { 
            var reportName=(obj as ReportModel)?.Name;

            if(string.IsNullOrEmpty(reportName)) return;

            ReportView view = new ReportView();
            var viewNodel = Injector.GetInstance<ReportViewModel>();
            view.DataContext = viewNodel;
            viewNodel.Init(view);
            view.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
