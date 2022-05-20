
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sanator.ViewModel
{
    class UchetViewModel : INotifyPropertyChanged
    {
        public DbOperations db;
        public ObservableCollection<Uchet> Uchets { get; set; }
        public ObservableCollection<Worker> Workers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        
        private Uchet selectedUchet;
        public Uchet SelectedUchet
        {
            get { return selectedUchet; }
            set
            {
                selectedUchet = value;
                OnPropertyChanged("SelectedUchet");
            }
        }
        IDialogService ds;
        public UchetViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            Uchets = new ObservableCollection<Uchet>(db.GetAllUchet().OrderBy((u)=>u.Client.FIO));
            Workers = new ObservableCollection<Worker>(db.GetAllWorker());
            Clients = new ObservableCollection<Client>(db.GetAllClient());
            SelectedUchet = Uchets.FirstOrDefault();
        }
        private RelayCommand addUchet;
        public RelayCommand AddUchet
        {
            get
            {
                return addUchet ??
                    (addUchet = new RelayCommand(obj =>
                    {
                        Uchet uchet = new Uchet() { Date_start = DateTime.Now, Date_finish = DateTime.Now, Worker= db.FindWorker(User.ID_worker)};
                        Uchets.Insert(0, uchet);
                        SelectedUchet = uchet;
                        ds.ShowMessage("Новый объект обавлен!");
                    }));
            }
        }
        private RelayCommand removeUchet;
        public RelayCommand RemoveUchet
        {
            get
            {
                return removeUchet ??
                    (removeUchet = new RelayCommand(obj =>
                    {

                        if (db.FindUchet(SelectedUchet.ID_ychet) != null)
                        {
                            db.RemoveUchet(SelectedUchet);
                            Uchets.Remove(SelectedUchet);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else Uchets.Remove(SelectedUchet);
                    },
                (obj) => SelectedUchet != null));
            }
        }
        private RelayCommand saveUchet;
        public RelayCommand SaveUchet
        {
            get
            {
                return saveUchet ??
                    (saveUchet = new RelayCommand(obj =>
                    {

                        var uchet = db.FindUchet(SelectedUchet.ID_ychet);
                        if (uchet == null)
                        {
                            db.AddUchet(SelectedUchet);
                        }
                        db.Save();
                        ds.ShowMessage("Изменения сохранены!");
                    },
                    (obj) => (SelectedUchet != null) && SelectedUchet.Client1 != null && SelectedUchet.Number1 != null && SelectedUchet.Worker1 != null));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
