

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanator.ViewModel
{
    class ReportViewModel : INotifyPropertyChanged
    {
        public DbOperations db;
        public ObservableCollection<Uchet> uchet { get; set; }
        public ObservableCollection<Uchet> report { get; set; }
        private int count;
        public int Count
        {
            get { return count;}
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        IDialogService ds;
        public ReportViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;           
            uchet = new ObservableCollection<Uchet>(db.GetAllUchet());
            report = new ObservableCollection<Uchet>();
            SelectedDateStart = DateTime.Now;
            SelectedDateFinish = DateTime.Now;
            Count = 0;
        }
        private DateTime selectedDateStart;
        public DateTime SelectedDateStart
        {
            get { return selectedDateStart; }
            set
            {
                selectedDateStart = value;
                OnPropertyChanged("SelectedDateStart");
            }
        }
        private DateTime selectedDateFinish;
        public DateTime SelectedDateFinish
        {
            get { return selectedDateFinish; }
            set
            {
                selectedDateFinish = value;
                OnPropertyChanged("SelectedDateFinish");
            }
        }
        private RelayCommand reportCommand;
        public RelayCommand ReportCommand
        {
            get
            {
                return reportCommand ??
                (reportCommand = new RelayCommand(obj =>
                {
                    if (report.Count != 0) report.Clear();

                    foreach (var item in uchet)
                    {
                        if (SelectedDateStart <= item.Date_start && SelectedDateFinish >= item.Date_finish && SelectedDateFinish >= item.Date_start && SelectedDateStart <= item.Date_finish)                 
                        {
                            Uchet item2 = item;
                            report.Insert(0,item2);
                        }
                    }
                    Count = report.Count();
                }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
