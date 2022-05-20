using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Sanator.ModelDb;
//using MySql.Data.MySqlService;
using System;

namespace Sanator.ViewModel
{
    public class WorkerViewModel : INotifyPropertyChanged
    {
        DbOperations db;
        private Worker selectedWorker;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Worker> workers { get; set; }
        IDialogService ds;

        public string FIO { get; set; }
        public string Position { get; set; }
        public string Passport { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
        public DateTime Birth { get; set; }

        public WorkerViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            workers = new ObservableCollection<Worker>(db.GetAllWorker());
            SelectedWorker=db.GetAllWorker().FirstOrDefault();

            SaveWorker = new RelayCommand(obj =>
            {
                if (SelectedWorker != null)
                {
                    var Worker = db.FindWorker(SelectedWorker.ID_worker);
                    if (Worker == null)
                    {
                        db.AddWorker(SelectedWorker);
                    }
                    db.Save();
                    ds.ShowMessage("Изменения сохранены!");

                }
            }, (obj) =>true);
        }
        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }
        private RelayCommand addWorker;
        public RelayCommand AddWorker
        {
            get
            {
                return addWorker ??
                    (addWorker = new RelayCommand(obj =>
                    {
                        Worker worker = new Worker() { Fio = "Новый сотрудник", Birth=DateTime.Now };
                        workers.Insert(0, worker);
                        SelectedWorker = worker;
                        ds.ShowMessage("Новый сотрудник добавлен!");
                    }));
            }
        }
        private RelayCommand removeWorker;
        public RelayCommand RemoveWorker
        {
            get
            {
                return removeWorker ??
                    (removeWorker = new RelayCommand(obj =>
                    {

                        if (db.FindWorker(SelectedWorker.ID_worker) != null)
                        {
                            db.RemoveWorker(SelectedWorker);
                            workers.Remove(SelectedWorker);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else workers.Remove(SelectedWorker);
                    },
                    (obj) => SelectedWorker != null));
            }
        }

        private readonly RelayCommand saveWorker;
        public RelayCommand SaveWorker { get; set; }
        /*{
            get
            {
                return saveWorker ??
                    (saveWorker = new RelayCommand(obj =>
                    {
                        if (SelectedWorker != null)
                        {
                            var Worker = db.FindWorker(SelectedWorker.ID_worker);
                            if (Worker == null)
                            {
                                db.AddWorker(SelectedWorker);
                            }
                            db.Save();
                            ds.ShowMessage("Изменения сохранены!");

                        }
                    },
                    (obj) => CanExecuteSave()));
            }
        }*/
        private bool CanExecuteSave()
        {
            if (SelectedWorker != null)
            {
                if (SelectedWorker.Fio != "Новый сотрудник" && SelectedWorker.Number != null && SelectedWorker.Passport != null) return true;
            }
            return false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        void RemoveClientById(int id)
        {
            MySqlDB.GetDB().ExecuteNonQuery("delete from Worker where ID_worker = " + id);
        }
    }
}
