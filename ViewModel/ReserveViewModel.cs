
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
    public class ReserveViewModel : INotifyPropertyChanged           
    {
        public DbOperations db;
        public ObservableCollection<Client> clients { get; set; }
        private Client selectedClient;

        public ObservableCollection<Kategory> kategory { get; set; }
        private Kategory selectedKatgory;
        
        private ObservableCollection<Number> numbers { get; set; }//all numbers
        private ObservableCollection<Number> numb;//номера с выбранным типом
        private Number selectedNumber;

        public ObservableCollection<Status> status { get; set; }
        private Status selectedStatus;

        public ObservableCollection<Uchet> uchets { get; set; }
        //private bool[] flag;

        public ObservableCollection<Worker> worker { get; set; }
        private Worker selectedWorker;
        IDialogService ds;
        public int Sum { get; set; }

        private string text_;
        public string text
        {
            get { return text_; }
            set
            {
                text_ = value;
                OnPropertyChanged("text");
            }
        }
        public ReserveViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            clients = new ObservableCollection<Client>(db.GetAllClient());
            kategory = new ObservableCollection<Kategory>(db.GetAllKategory());
            status = new ObservableCollection<Status>(db.GetAllStatus());
            date_start = DateTime.Now;
            date_finish =DateTime.Now;
            worker = new ObservableCollection<Worker>(db.GetAllWorker());
            uchets = new ObservableCollection<Uchet>(db.GetAllUchet());
            SelectedWorker = db.FindWorker(User.ID_worker);
        }
        private DateTime _date_start;
        public DateTime date_start
        {
            get { return _date_start; }
            set
            {
                _date_start = value;
                SetSum();
                OnPropertyChanged("date_start");
            
            }
        }
        private DateTime _date_finish;
        public DateTime date_finish
        {
            get { return _date_finish; }
            set
            {
                _date_finish = value;
                SetSum();
                OnPropertyChanged("date_finish");
            }
        }
        private RelayCommand reserve;
        public RelayCommand Reserve
        {
            get
            {
                return reserve ??
                    (reserve = new RelayCommand(obj =>
                    {
                        try
                        {
                            var items = db.GetAllUchet().Where(u => u.ID_number_FK == SelectedNumber.ID);
                            foreach (var item in items) { if (item.date_start <= date_start && date_start <= item.date_finish) { text = "Выбранный вами номер забронирован на этот период!"; return; } }
                            if (SelectedClient != null)
                            {
                                if (SelectedNumber != null)
                                {
                                    if (SelectedWorker != null)
                                    {
                                        if (date_start < date_finish)
                                        {
                                            var client = db.FindClient(SelectedClient.ID_client);
                                            var number = db.FindNumber(SelectedNumber.ID_number);
                                            var worker = db.FindWorker(SelectedWorker.ID_worker);
                                            var uchett = new Uchet()
                                            {
                                                ID_client_FK = client.ID_client,
                                                ID_number_FK = number.ID_number,
                                                ID_worker_FK = worker.ID_worker,
                                                Date_start = date_start,
                                                Date_finish = date_finish
                                            };
                                            if (date_start <= DateTime.Today && DateTime.Today <= date_finish) number.Status = db.FindStatus(2);
                                            db.AddUchet(uchett);
                                            db.Save();
                                            text = "";
                                            MessageBox.Show("Номер забронирован!");
                                    
                                        }
                                        else text = "Период проживания указан неправильно!";
                                    }
                                    else text = "Пожалуйста, выберете администратора, работающего в системе!";
                                }
                                else text = "Пожалуйста, выберете номер для бронирования!";
                            }
                            else text = "Пожалуйста, выберете клиента!";
                        }
                        catch { ds.ShowMessage("Номер не может быть забронирован!"); }
                    },obj=> SelectedNumber != null && SelectedWorker != null && SelectedClient!=null));
            }
        }
        public Status SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                Numbers = new ObservableCollection<Number>(numbers.Where(u => u.Kategory == SelectedKatgory && u.Status==SelectedStatus).ToList());
                OnPropertyChanged("SelectedStatus");
            }
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

        public Number SelectedNumber
        {
            get { return selectedNumber; }
            set
            {
                selectedNumber = value;
                OnPropertyChanged("SelectedNumber");
            }
        }
        public ObservableCollection<Number> Numbers
        {
            get
            {                
                return numb;
            }
            set
            {
                numb = value;               
                OnPropertyChanged("Numbers");
            }
        }
        public Kategory SelectedKatgory
        {
            get { return selectedKatgory; }
            set
            {
                selectedKatgory = value;
                Numbers = new ObservableCollection<Number>(numbers.Where(u => u.Kategory == SelectedKatgory && u.Status == SelectedStatus).ToList());
                SetSum();
                OnPropertyChanged("SelectedKatgory");
                OnPropertyChanged("Sum");

            }
        }
        private void SetSum()
        {
            if (SelectedKatgory != null)
            {
              Sum = SelectedKatgory.Cost * (date_finish - date_start).Days;
              if (Sum == 0) Sum = SelectedKatgory.Cost;
                OnPropertyChanged("Sum");
            }
  
        }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
