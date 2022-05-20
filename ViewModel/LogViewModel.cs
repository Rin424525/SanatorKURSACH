

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sanator.ViewModel
{
    class LogViewModel : INotifyPropertyChanged
    {
        private Log selectedLog;
        public DbOperations db;
        public ObservableCollection<Log> log { get; set; }
        public ObservableCollection<Client> clients { get; set; }
        public ObservableCollection<Service> services { get; set; }
        IDialogService ds;
        public LogViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;

            log = new ObservableCollection<Log>(this.db.GetAllLog());
            clients = new ObservableCollection<Client>(this.db.GetAllClient());
            services = new ObservableCollection<Service>(this.db.GetAllService());
            SelectedLog = log.FirstOrDefault();
        }

        public Log SelectedLog
        {
            get { return selectedLog; }
            set
            {
                selectedLog = value;
                OnPropertyChanged("SelectedLog");

            }
        }
        private RelayCommand removeLog;
        public RelayCommand RemoveLog
        {
            get
            {
                return removeLog ??
                    (removeLog = new RelayCommand(obj =>
                    {

                        if (db.FindLog(SelectedLog.ID_line) != null)
                        {
                            db.RemoveLog(SelectedLog);
                            log.Remove(SelectedLog);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else log.Remove(SelectedLog);
                    },
                    (obj) => SelectedLog != null));
            }
        }
        private RelayCommand addLog;
        public RelayCommand AddLog
        {
            get
            {
                return addLog ??
                    (addLog = new RelayCommand(obj =>
                    {
                        Log _log = new Log() { Client1 = new Client() { Fio = "Новый объект" }, Date = DateTime.Now };
                        log.Insert(0, _log);
                        SelectedLog = _log;
                        ds.ShowMessage("Новый объект добавлен!");
                    }));
            }
        }
        private RelayCommand saveLog;
        public RelayCommand SaveLog
        {
            get
            {
                return saveLog ??
                    (saveLog = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (SelectedLog != null)
                            {
                                var log = db.FindLog(SelectedLog.ID_line);
                                if (log == null)
                                {
                                    db.AddLog(SelectedLog);
                                }

                                db.Save();
                                ds.ShowMessage("Изменения сохранены!");
                            }
                            else ds.ShowMessage("Пожалуйста, выберете запись из списка!");
                        }
                        catch
                        {
                            ds.ShowMessage("Невозможно изменить объект!");
                        }

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
